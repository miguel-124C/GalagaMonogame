using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Managers;
using Microsoft.Xna.Framework;

namespace Galaga.Factories
{
    public class EntityFactory(EntityManager em, SpriteAtlas sa)
    {
        private readonly EntityManager entityManager = em;
        private readonly SpriteAtlas spriteAtlas = sa;

        public uint CreatePlayer(Vector2 position)
        {
            var player = entityManager.CreateEntity();

            var transform = new Transform
            {
                Position = position,
                Rotation = 0f,
                Scale = new Vector2(3f, 3f)
            };
            entityManager.AddComponent(player, transform);

            var sprite = spriteAtlas.GetSprite("Nave");
            var spriteWidth = sprite.SourceRectangle.Width * transform.Scale.X;
            var spriteHeight = sprite.SourceRectangle.Height * transform.Scale.Y;

            var offset = new Vector2(1, 1) * transform.Scale;
            var collider = new Collider
            {
                Width = (int)((spriteWidth) - (offset.X * 2)),
                Height = (int)((spriteHeight) - (offset.Y * 2)),
                Offset = offset
            };

            entityManager.AddComponent(player, sprite);
            entityManager.AddComponent(player, new Player());
            entityManager.AddComponent(player, collider);
            entityManager.AddComponent(player, new Health { Max = 3, Current = 3 });
            entityManager.AddComponent(player, new Physics());

            var spawnOffset = new Vector2(spriteWidth / 2, 0);
            entityManager.AddComponent(player, new Weapon{
                CoolDown = 0f, FireRate = 0.5f, SpawnOffset = spawnOffset
            });

            return player;
        }
    }
}