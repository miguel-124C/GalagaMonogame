using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Managers;
using Microsoft.Xna.Framework;

namespace Galaga.Factories
{
    public class EnemyFactory(EntityManager em, SpriteAtlas sa)
    {
        private readonly EntityManager entityManager = em;
        private readonly SpriteAtlas spriteAtlas = sa;

        public uint CreateBoos(Vector2 position)
        {
            var boos = AssembleBaseEnemy(position, "Enemy_Boos_Green_Fly");
            entityManager.AddComponent(boos, new Boos());
            entityManager.AddComponent(boos, new Health { Max = 2, Current = 2 });

            return boos;
        }

        public uint CreateBee(Vector2 position)
        {
            var bee = AssembleBaseEnemy(position, "Enemy_Bee_Fly");
            entityManager.AddComponent(bee, new Bee());
            entityManager.AddComponent(bee, new Health { Max = 1, Current = 1 });

            return bee;
        }

        public uint CreateButterfly(Vector2 position)
        {
            var butterfly = AssembleBaseEnemy(position, "Enemy_Butterfly_Fly");
            entityManager.AddComponent(butterfly, new Butterfly());
            entityManager.AddComponent(butterfly, new Health { Max = 1, Current = 1 });

            return butterfly;
        }

        private uint AssembleBaseEnemy(Vector2 position, string spriteName)
        {
            var baseEnemy = entityManager.CreateEntity();
            entityManager.AddComponent(baseEnemy, new Transform
            {
                Position = position,
                Rotation = 0f,
                Scale = new Vector2(1f, 1f)
            });

            var sprite = spriteAtlas.GetSprite(spriteName);

            var offset = new Vector2(1, 1);
            var collider = new Collider
            {
                Width = (int)(sprite.SourceRectangle.Width - (offset.X * 2)),
                Height = (int)(sprite.SourceRectangle.Height - (offset.Y * 2)),
                Offset = offset
            };

            entityManager.AddComponent(baseEnemy, sprite);
            entityManager.AddComponent(baseEnemy, new Enemy());
            entityManager.AddComponent(baseEnemy, collider);
            entityManager.AddComponent(baseEnemy, new Physics());

            return baseEnemy;
        }
    }
}