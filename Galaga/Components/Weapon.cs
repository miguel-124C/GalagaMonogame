using Galaga.Core.ECS;

namespace Galaga.Components
{
    public struct Weapon : IComponent
    {
        public float FireRate; // Disparos por segundo
        public float CoolDown; // Temporizador interno
    }
}