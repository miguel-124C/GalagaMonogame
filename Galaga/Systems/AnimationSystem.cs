using Galaga.Components;
using Galaga.Core.ECS;
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