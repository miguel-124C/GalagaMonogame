using Galaga.Core.ECS;
using Microsoft.Xna.Framework;

namespace Galaga.Components
{
    public struct EnemyStateData : IComponent
    {
        public float TimeInFormation;
        public Vector2 FormationPosition; // Su "hogar" en la cuadrícula superior
        public Vector2 TargetPosition;    // Hacia dónde está volando actualmente
    }
}