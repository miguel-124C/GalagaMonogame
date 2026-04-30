using Galaga.Core.ECS;
using System.Collections.Generic;

namespace Galaga.Managers
{
    public class SystemsManager
    {
        private readonly List<ISystem> systems = [];

        public void RegisterSystem(ISystem system) => systems.Add(system);

        public void UpdateSystems(float deltaTime)
        {
            foreach (var system in systems)
                system.Update(deltaTime);
        }

        public void InitializeSystems(EntityManager entityManager)
        {
            foreach (var system in systems)
                system.Initialize(entityManager);
        }
    }
}