using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Interfaces;
using Galaga.Managers;
using Microsoft.Xna.Framework;

namespace Galaga.Systems
{
    public class EnemyAISystem(SpriteAtlas spriteAtlas, GraphicsDeviceManager gdm) : ISystem
    {
        private readonly float screenWidth = gdm.PreferredBackBufferWidth;

        public override void Update(float deltaTime)
        {
            var enemies = EntityManager.GetEntitiesWith<Enemy>();

            foreach (var enemy in enemies)
            {
                var stateData = EntityManager.GetComponent<EnemyStateData>(enemy);
                var transform = EntityManager.GetComponent<Transform>(enemy);
                
                switch (stateData.State)
                {
                    case EnemyState.Entering:
                        HandleEnteringState(enemy, deltaTime);
                        break;
                    case EnemyState.InFormation:
                        ChangeEnemyDirection(enemy, transform);
                        HandleInFormationState(enemy, transform, deltaTime);
                        break;
                    case EnemyState.Diving:
                        HandleDivingState(enemy, stateData, transform, deltaTime);
                        break;
                }

                //EntityManager.AddComponent(enemy, stateData);
            }
        }

        private void HandleEnteringState(uint enemy, float deltaTime)
        {
            var stateData = EntityManager.GetComponent<EnemyStateData>(enemy);
            var transform = EntityManager.GetComponent<Transform>(enemy);
            stateData.Progress += deltaTime;

            var time = stateData.Progress / stateData.DurationInState;
            if (time >= 1)
            {
                string nameSprite = "";
                // TODO: Is Boos? or Boss?
                if (EntityManager.HasComponent<Butterfly>(enemy))
                    nameSprite = "Enemy_Butterfly_Rotation";
                else if (EntityManager.HasComponent<Bee>(enemy))
                    nameSprite = "Enemy_Bee_Rotation";
                else if (EntityManager.HasComponent<Boos>(enemy))
                    nameSprite = "Enemy_Boos_Rotation";

                var sprite = spriteAtlas.GetSprite(nameSprite);
                sprite.TimePerFrame = 0.3f;

                EntityManager.AddComponent(enemy, sprite);
                stateData.State = EnemyState.InFormation;
                EntityManager.AddComponent(enemy, stateData);
                return;
            }

            var Bezier = DeCasteljau(stateData.PointsControl, time);
            transform.Position = Bezier;
            
            EntityManager.AddComponent(enemy, transform);
            EntityManager.AddComponent(enemy, stateData);
        }

        private void HandleInFormationState
            (uint enemy, Transform transform, float deltaTime)
        {
            var swarmData = EntityManager.GetComponent<SwarmData>(enemy);

            transform.Position.X = (swarmData.Direction == EnemyDirection.Right)
                ? transform.Position.X + 50 * deltaTime
                : transform.Position.X - 50 * deltaTime;
            
            EntityManager.AddComponent(enemy, transform);
        }

        private void HandleDivingState
            (uint enemy, EnemyStateData stateData, Transform transform, float deltaTime)
        {
            stateData.Progress += deltaTime;
            var time = stateData.Progress / stateData.DurationInState;
            transform.Position = DeCasteljau(stateData.PointsControl, time);

            if (time >= 1)
            {
                stateData.State = EnemyState.Entering;
                stateData.Progress = 0;
                stateData.DurationInState = 1;
                stateData.PointsControl = [
                    new(stateData.PositionBeforeDiving.X, 0),
                    new(stateData.PositionBeforeDiving.X, 0),
                    stateData.PositionBeforeDiving,
                    stateData.PositionBeforeDiving
                ];
                return;
            }

            EntityManager.AddComponent(enemy, transform);
        }

        private void ChangeEnemyDirection(uint enemy, Transform transform)
        {
            var sprite = EntityManager.GetComponent<Sprite>(enemy);
            var spriteWidth = sprite.SourceRectangle.Width * transform.Scale.X;
            var swarmData = EntityManager.GetComponent<SwarmData>(enemy);

            if (transform.Position.X <= 0)
                swarmData.Direction = EnemyDirection.Right;
            else if (transform.Position.X + spriteWidth >= screenWidth)
                swarmData.Direction = EnemyDirection.Left;

            var swarm = EntityManager.GetEntitiesWith<SwarmData>();
            foreach (var item in swarm)
            {
                EntityManager.AddComponent(item, new SwarmData
                {
                    Direction = swarmData.Direction
                });
            }

            EntityManager.AddComponent(enemy, swarmData);
        }

        private static Vector2 DeCasteljau(Vector2[] points, float t)
        {
            Vector2[] pts = (Vector2[])points.Clone();
            int n = pts.Length;

            for (int level = 1; level < n; level++)
                for (int i = 0; i < n - level; i++)
                    pts[i] = Vector2.Lerp(pts[i], pts[i + 1], t); // (1-t)*pts[i] + t*pts[i+1]

            return pts[0];
        }
    }
}