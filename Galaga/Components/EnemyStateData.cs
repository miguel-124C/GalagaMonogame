using Galaga.Core.ECS;
using Galaga.Interfaces;
using Microsoft.Xna.Framework;

namespace Galaga.Components
{
    public struct EnemyStateData : IComponent
    {
        public float DurationInState;     // Duración del enemigo en un estado específico
        public Vector2[] PointsControl;   // Puntos de control para el movimiento de entrada
        public float Progress;            // Progreso del movimiento de entrada (0 a 1)
        public Vector2 PositionBeforeDiving; // Posición del enemigo antes de iniciar el dive
        public EnemyState State;          // Estado actual del enemigo
    }
}