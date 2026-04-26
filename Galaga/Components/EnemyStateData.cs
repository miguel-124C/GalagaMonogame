using Galaga.Core.ECS;
using Galaga.Interfaces;
using Microsoft.Xna.Framework;

namespace Galaga.Components
{
    public struct EnemyStateData : IComponent
    {
        public float TimeInFormation;
        public Vector2 InitialPosition;   // Posición de entrada para el enemigo
        public Vector2[] PointsControl;   // Puntos de control para el movimiento de entrada
        public Vector2 TargetPosition;    // Hacia dónde está volando actualmente
        public float Progress;            // Progreso del movimiento de entrada (0 a 1)
        public EnemyState State;          // Estado actual del enemigo
    }
}