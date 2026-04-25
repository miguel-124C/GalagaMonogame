using Galaga.Components;
using Galaga.Core.ECS;
using System.Linq;

namespace Galaga.Systems
{
    public class MovementSystem : ISystem
    {
        public override void Update(float deltaTime)
        {
            var physicsEntities = EntityManager.GetEntitiesWith<Physics>();
            if (!physicsEntities.Any()) return;

            foreach (var entity in physicsEntities)
            {
                if (!EntityManager.HasComponent<Transform>(entity))
                    continue;

                var transformComponent = EntityManager.GetComponent<Transform>(entity);
                var physicsComponent = EntityManager.GetComponent<Physics>(entity);

                transformComponent.Position += physicsComponent.Velocity * deltaTime;
            }
        }
    }
}