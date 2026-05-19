using Microsoft.Xna.Framework;

namespace Galaga.Interfaces
{
    public struct EnemyProps
    {
        public Vector2 Position;
        public Vector2[] PointsControl;
        public bool Visible;
        public float DelaySpawn;
    }
}