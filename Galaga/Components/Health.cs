using Galaga.Core.ECS;

namespace Galaga.Components
{
    public struct Health : IComponent
    {
        public int Current, Max;
    }
}