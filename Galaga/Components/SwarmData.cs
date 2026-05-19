using Galaga.Core.ECS;
using Galaga.Interfaces;
using Microsoft.Xna.Framework;

namespace Galaga.Components
{
    public struct SwarmData : IComponent
    {
        public Vector2 PositionMatrix;
        public EnemyDirection Direction;
    }
}