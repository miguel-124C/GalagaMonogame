using Galaga.Core.ECS;
using Galaga.Components;
using Microsoft.Xna.Framework;

namespace Galaga.Systems
{
    internal class BulletSystem(GraphicsDeviceManager gdm) : ISystem
    {
        private readonly int ScreenWidth = gdm.PreferredBackBufferWidth;
        private readonly int ScreenHeight = gdm.PreferredBackBufferHeight;
        public override void Update(float deltaTime)
        {
            var bullets = EntityManager.GetEntitiesWith<Bullet>();

            foreach (var bullet in bullets)
            {
                var transform = EntityManager.GetComponent<Transform>(bullet);
                var sprite = EntityManager.GetComponent<Sprite>(bullet);
                
                var spriteWidth = sprite.SourceRectangle.Width * transform.Scale.Y;
                var spriteHeight = sprite.SourceRectangle.Height * transform.Scale.Y;

                var position = transform.Position;

                var isGetOutUp = position.Y + spriteHeight <= 0;
                var isGetOutDown = position.Y - spriteHeight >= ScreenHeight;
                var isGetOutLeft = position.X + spriteWidth <= 0;
                var isGetOutRight = position.X - spriteWidth >= ScreenWidth;

                if (isGetOutUp || isGetOutDown || isGetOutLeft || isGetOutRight)
                    EntityManager.AddComponent(bullet, new DestroyTag());
            }
        }
    }
}