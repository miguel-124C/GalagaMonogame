using Galaga.Core.ECS;
using Microsoft.Xna.Framework;

namespace Galaga.Components
{
    public struct Weapon : IComponent
    {
        public Vector2 SpawnOffset;
        public float FireRate; // Disparos por segundo
        public float CoolDown; // Temporizador interno
    }
}