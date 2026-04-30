using Galaga.Core.ECS;
using Galaga.Components;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Galaga.Systems
{
    public class RenderSystem(SpriteBatch sp) : ISystem
    {
        private readonly SpriteBatch _spriteBatch = sp;
        public override void Update(float deltaTime)
        {
            var spriteEntities = EntityManager.GetEntitiesWith<Sprite>();
            if (!spriteEntities.Any()) return;

            // Esto se ponia cuando el fondo era negro... se ponia esto para que no
            // se vean bordes grises... pero quitando el fondo negro y pasandolo a
            // transparente se quitó
            //_spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            _spriteBatch.Begin();

            foreach (var entity in spriteEntities)
            {
                if (!EntityManager.HasComponent<Transform>(entity))
                    continue;

                var sprite = EntityManager.GetComponent<Sprite>(entity);
                var transform = EntityManager.GetComponent<Transform>(entity);

                var spriteWidth = sprite.SourceRectangle.Width;

                var curretFrame = sprite.CurrentFrame;
                var gap = sprite.GapPerFrame;

                sprite.SourceRectangle.X += (int)(curretFrame * (spriteWidth + gap));

                _spriteBatch.Draw(
                    sprite.Texture,
                    transform.Position,
                    sprite.SourceRectangle,
                    Color.White,
                    transform.Rotation,
                    Vector2.Zero,
                    transform.Scale,
                    sprite.SpriteEffect,
                    0f
                );
            }

            _spriteBatch.End();
        }
    }
}