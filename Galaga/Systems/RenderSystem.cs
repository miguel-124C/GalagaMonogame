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

            _spriteBatch.Begin();

            foreach (var entity in spriteEntities)
            {
                if (!EntityManager.HasComponent<Transform>(entity))
                    continue;

                var spriteComponent = EntityManager.GetComponent<Sprite>(entity);
                var transformComponent = EntityManager.GetComponent<Transform>(entity);
                
                _spriteBatch.Draw(
                    spriteComponent.Texture,
                    transformComponent.Position,
                    spriteComponent.SourceRectangle,
                    Color.White,
                    transformComponent.Rotation,
                    Vector2.Zero,
                    transformComponent.Scale,
                    spriteComponent.SpriteEffect,
                    0f
                );
            }

            _spriteBatch.End();
        }
    }
}