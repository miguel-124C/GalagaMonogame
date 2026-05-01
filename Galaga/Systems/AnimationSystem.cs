using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Galaga.Systems
{
    public class AnimationSystem : ISystem
    {
        public override void Update(float deltaTime)
        {
            var explosions = EntityManager.GetEntitiesWith<Explosion>();
            if (explosions.Any())
                ExplosionAnimation(deltaTime, explosions);

            // TODO: EntityManager.GetEntitiesWith<SwarmData, EnemyStateData>();
            var swarmData = EntityManager.GetEntitiesWith<EnemyStateData>();
            if (swarmData.Any())
            {
                foreach (var entity in swarmData)
                {
                    var enemyStateData = EntityManager.GetComponent<EnemyStateData>(entity);

                    switch (enemyStateData.State)
                    {
                        case EnemyState.Entering:
                            SwarmEnteringAnimation(deltaTime, entity);
                            break;
                        case EnemyState.InFormation:
                            SwarmInFormationAnimation(deltaTime, entity);
                            break;
                        case EnemyState.Diving:
                            SwarmDivingAnimation(deltaTime, entity);
                            break;
                    };
                }
            }
        }

        private void SwarmEnteringAnimation(float deltaTime, uint entity)
        {
            var sprite = EntityManager.GetComponent<Sprite>(entity);
            sprite.TimeElapsed += deltaTime;
            if (sprite.TimeElapsed >= sprite.TimePerFrame){}
        }

        private void SwarmInFormationAnimation(float deltaTime, uint entity)
        {
            var sprite = EntityManager.GetComponent<Sprite>(entity);
            sprite.TimeElapsed += deltaTime;
            if (sprite.TimeElapsed >= sprite.TimePerFrame)
            {
                if (sprite.CurrentFrame == sprite.TotalFrames - 1)
                    sprite.CurrentFrame = 0;
                else
                    sprite.CurrentFrame += 1;

                sprite.TimeElapsed = 0;
            }

            EntityManager.AddComponent(entity, sprite);
        }

        private void SwarmDivingAnimation(float deltaTime, uint entity)
        {
            var sprite = EntityManager.GetComponent<Sprite>(entity);
            sprite.TimeElapsed += deltaTime;
            if (sprite.TimeElapsed >= sprite.TimePerFrame){}
        }

        private void ExplosionAnimation(float deltaTime, IEnumerable<uint> explosions)
        {
            foreach (var entity in explosions)
            {
                var sprite = EntityManager.GetComponent<Sprite>(entity);
                sprite.TimeElapsed += deltaTime;

                if (sprite.CurrentFrame >= sprite.TotalFrames)
                {
                    sprite.CurrentFrame = 0;
                    EntityManager.AddComponent(entity, new DestroyTag());
                    continue;
                }

                if (sprite.TimeElapsed >= sprite.TimePerFrame)
                {
                    sprite.CurrentFrame += 1;
                    sprite.TimeElapsed = 0;
                }

                EntityManager.AddComponent(entity, sprite);
            }
        }
    }
}