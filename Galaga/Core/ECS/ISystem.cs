namespace Galaga.Core.ECS
{
    public abstract class ISystem
    {
        protected EntityManager EntityManager { get; private set; }
        public void Initialize(EntityManager em) => EntityManager = em;
        public abstract void Update(float deltaTime);
    }
}