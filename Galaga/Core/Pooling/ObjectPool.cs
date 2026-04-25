using System.Collections.Generic;

namespace Galaga.Core.Pooling
{
    public class ObjectPool<T> where T : class, new()
    {
        private readonly Stack<T> _pool = new();

        public T Get()
        {
            return (_pool.Count > 0)
                ? _pool.Pop()
                : new T();
        }

        public void Return(T item) => _pool.Push(item);
    }
}