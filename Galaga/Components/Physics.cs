using Galaga.Core.ECS;
using Microsoft.Xna.Framework;

namespace Galaga.Components
{
    public struct Physics : IComponent
    {
        public Vector2 Velocity, Acceleration;
    }
}