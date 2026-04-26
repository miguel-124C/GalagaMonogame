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

            entityManager.AddComponent(player, new Transform
            {
                Position = position,
                Rotation = 0f,
                Scale = new Vector2(1f, 1f)
            });

            var sprite = spriteAtlas.GetSprite("Nave");
            var spriteWidth = sprite.SourceRectangle.Width;

            var offset = new Vector2(1, 1);
            var collider = new Collider
            {
                Width = (int)(spriteWidth - (offset.X * 2)),
                Height = (int)(sprite.SourceRectangle.Height - (offset.Y * 2)),
                Offset = offset
            };

            entityManager.AddComponent(player, sprite);
            entityManager.AddComponent(player, new Player());
            entityManager.AddComponent(player, collider);
            entityManager.AddComponent(player, new Health { Max = 3, Current = 3 });
            entityManager.AddComponent(player, new Physics());

            var spawnOffset = new Vector2(spriteWidth / 2, 0);
            entityManager.AddComponent(player, new Weapon{
                CoolDown = 0.5f, FireRate = 0.5f, SpawnOffset = spawnOffset
            });

            return player;
        }
    }
}