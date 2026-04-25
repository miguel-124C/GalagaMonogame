using Galaga.Core.ECS;
using Microsoft.Xna.Framework;

namespace Galaga.Components
{
    public struct Collider : IComponent
    {
        public int Width, Height;
        public Vector2 Offset;
    }
}