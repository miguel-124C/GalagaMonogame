using System;
using System.Collections.Generic;
using System.Linq;
using Entity = uint;

namespace Galaga.Core.ECS
{
    public class EntityManager
    {
        private Entity nextEntityId = 0;
        private readonly Dictionary<Entity, Dictionary<Type, IComponent>> _entities = [];
        
        public Entity CreateEntity()
        {
            var entity = nextEntityId++;
            _entities[entity] = [];
            return entity;
        }

        public void AddComponent<T>(Entity entity, T component) where T : IComponent
        {
            _entities[entity][typeof(T)] = component;
        }

        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            return (T)_entities[entity][typeof(T)];
        }

        public bool HasComponent<T>(Entity entity) where T : IComponent
        {
            return _entities[entity].ContainsKey(typeof(T));
        }

        public IEnumerable<Entity> GetEntitiesWith<T>() where T : IComponent
        {
            return _entities.Where(e => e.Value.ContainsKey(typeof(T)))
                .Select(e => e.Key);
        }
    }
}