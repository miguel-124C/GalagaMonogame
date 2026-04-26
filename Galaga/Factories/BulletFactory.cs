using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Interfaces;
using Galaga.Managers;
using Microsoft.Xna.Framework;

namespace Galaga.Factories
{
    public class BulletFactory(EntityManager em, SpriteAtlas sa)
    {
        private readonly EntityManager entityManager = em;
        private readonly SpriteAtlas spriteAtlas = sa;

        public uint CreateBullet(Vector2 position, BulletType bulletType)
        {
            var isPlayerBullet = bulletType == BulletType.PlayerBullet;

            IComponent tagComponent = (isPlayerBullet)
                ? new PlayerBullet()
                : new EnemyBullet();

            string spriteName = (isPlayerBullet)
                ? "Player_Bullet"
                : "Enemy_Bullet";

            var bullet = entityManager.CreateEntity();

            entityManager.AddComponent(bullet, new Transform {
                Position = position, Rotation = 0f, Scale = new Vector2(1f, 1f)
            });
            entityManager.AddComponent(bullet, tagComponent);

            var sprite = spriteAtlas.GetSprite(spriteName);
            // Asumiendo que el sprite del enemigo está en la segunda posición del atlas
            sprite.CurrentFrame = (!isPlayerBullet) ? 1 : 0;

            var offset = new Vector2(6, 4);
            var collider = new Collider
            {
                // Se le resta 1 por que el ancho del sprite es 16 y el offset es 6, entonces 16 - (6*2) = 4,
                //pero el ancho del collider debe ser 3, por eso se le resta 1
                Width = (int)(sprite.SourceRectangle.Width - (offset.X * 2) - 1),
                Height = (int)(sprite.SourceRectangle.Height - (offset.Y * 2)),
                Offset = offset
            };

            entityManager.AddComponent(bullet, sprite);
            entityManager.AddComponent(bullet, collider);

            var velocity = (isPlayerBullet)
                ? new Vector2(0, -300f)
                : new Vector2(0, 300f);
            entityManager.AddComponent(bullet, new Physics{ Velocity = velocity });

            return bullet;
        }
    }
}