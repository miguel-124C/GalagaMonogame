using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Interfaces;
using Galaga.Managers;
using Microsoft.Xna.Framework;

namespace Galaga.Factories
{
    public class EnemyFactory(EntityManager em, SpriteAtlas sa)
    {
        private readonly EntityManager entityManager = em;
        private readonly SpriteAtlas spriteAtlas = sa;

        public uint CreateBoos(Vector2 positionInitial, Vector2[] pointsControl)
        {
            var boos = AssembleBaseEnemy(positionInitial, pointsControl, "Enemy_Boos_Green_Fly");
            entityManager.AddComponent(boos, new Boos());
            entityManager.AddComponent(boos, new Health { Max = 2, Current = 2 });
            entityManager.AddComponent(boos, new ScoreValue { Value = 150 });

            return boos;
        }

        public uint CreateBee(Vector2 position, Vector2[] pointsControl)
        {
            var bee = AssembleBaseEnemy(position, pointsControl, "Enemy_Bee_Fly");
            entityManager.AddComponent(bee, new Bee());
            entityManager.AddComponent(bee, new Health { Max = 1, Current = 1 });
            entityManager.AddComponent(bee, new ScoreValue { Value = 50 });

            return bee;
        }

        public uint CreateButterfly(Vector2 position, Vector2[] pointsControl)
        {
            var butterfly = AssembleBaseEnemy(position, pointsControl, "Enemy_Butterfly_Fly");
            entityManager.AddComponent(butterfly, new Butterfly());
            entityManager.AddComponent(butterfly, new Health { Max = 1, Current = 1 });
            entityManager.AddComponent(butterfly, new ScoreValue { Value = 80 });

            return butterfly;
        }

        private uint AssembleBaseEnemy(Vector2 position, Vector2[] pointsControl, string spriteName)
        {
            var baseEnemy = entityManager.CreateEntity();
            var scale = new Vector2(3f, 3f);
            entityManager.AddComponent(baseEnemy, new Transform
            {
                Position = position,
                Rotation = 0f,
                Scale = scale
            });

            var sprite = spriteAtlas.GetSprite(spriteName);
            sprite.TimePerFrame = 0.3f;

            var offset = new Vector2(1, 1) * scale;
            var collider = new Collider
            {
                Width = (int)((sprite.SourceRectangle.Width * scale.X) - (offset.X * 2)),
                Height = (int)((sprite.SourceRectangle.Height * scale.Y) - (offset.Y * 2)),
                Offset = offset
            };

            entityManager.AddComponent(baseEnemy, sprite);
            entityManager.AddComponent(baseEnemy, new Enemy());
            entityManager.AddComponent(baseEnemy, collider);
            entityManager.AddComponent(baseEnemy, new EnemyStateData
            {
                Progress = 0,
                State = EnemyState.InFormation,
                DurationInState = 3f,
                PointsControl = pointsControl,
            });
            entityManager.AddComponent(baseEnemy, new SwarmData {
                Direction = EnemyDirection.Right
            });

            return baseEnemy;
        }
    }
}