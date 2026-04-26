using Microsoft.Xna.Framework;
using System;

namespace Galaga.Core.Events
{
    public static class EventManager
    {
        public static event Action OnPlayerDeath;
        public static event Action<int> OnEnemyDestroyed;
        public static event Action<Vector2> OnPlayerShoot;

        public static void TriggerPlayerDeath() => OnPlayerDeath?.Invoke();
        public static void TriggerEnemyDestroyed(int scoreValue)
            => OnEnemyDestroyed?.Invoke(scoreValue);

        public static void TriggerPlayerShoot(Vector2 position)
            => OnPlayerShoot?.Invoke(position);
    }
}