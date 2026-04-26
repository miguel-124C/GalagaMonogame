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
        private readonly Queue<uint> _recycledIds = [];

        public Entity CreateEntity()
        {
            var entity = (_recycledIds.Count > 0)
                ? _recycledIds.Dequeue()
                : nextEntityId++;

            _entities[entity] = [];
            return entity;
        }

        public void AddComponent<T>(Entity entity, T component) where T : IComponent
        {
            _entities[entity][typeof(T)] = component;
        }

        public void DestroyEntity(Entity id)
        {
            if (!_entities.TryGetValue(id, out var components)) return;

            components.Clear();
            _entities.Remove(id);
            _recycledIds.Enqueue(id);
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