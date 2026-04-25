using Galaga.Core.ECS;
using Microsoft.Xna.Framework;

namespace Galaga.Components
{
    public struct Transform : IComponent
    {
        public Vector2 Position, Scale;
        public float Rotation;
    }
}