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

            string spriteName = (isPlayerBullet)
                ? "Player_Bullet"
                : "Enemy_Bullet";

            var bullet = entityManager.CreateEntity();

            var sprite = spriteAtlas.GetSprite(spriteName);
            // Asumiendo que el sprite del enemigo está en la segunda posición del atlas
            sprite.CurrentFrame = (!isPlayerBullet) ? 1 : 0;

            var scale = new Vector2(3f, 3f);
            var spriteWidth = sprite.SourceRectangle.Width * scale.X;
            var spriteHeight = sprite.SourceRectangle.Height * scale.Y;

            var spawnBullet = new Vector2
            {
                X = position.X - (spriteWidth / 2),
                Y = position.Y - (spriteHeight)
            };
            var transform = new Transform
            {
                Position = spawnBullet,
                Rotation = 0f,
                Scale = scale
            };

            entityManager.AddComponent(bullet, transform);
            entityManager.AddComponent(bullet, new Bullet());

            if (isPlayerBullet)
                entityManager.AddComponent(bullet, new PlayerBullet());
            else
                entityManager.AddComponent(bullet, new EnemyBullet());

            var offset = new Vector2(6, 4) * scale;
            var collider = new Collider
            {
                // Se le resta 1 por que el ancho del sprite es 16 y el offset es 6, entonces 16 - (6*2) = 4,
                //pero el ancho del collider debe ser 3, por eso se le resta 1
                Width = (int)((spriteWidth) - (offset.X * 2) - 1),
                Height = (int)((spriteHeight) - (offset.Y * 2)),
                Offset = offset
            };

            entityManager.AddComponent(bullet, sprite);
            entityManager.AddComponent(bullet, collider);

            var velocity = (isPlayerBullet)
                ? new Vector2(0, -400f)
                : new Vector2(0, 300f);
            entityManager.AddComponent(bullet, new Physics{ Velocity = velocity });

            return bullet;
        }
    }
}