using System;

namespace Galaga.Core.Events
{
    public static class EventManager
    {
        public static event Action OnPlayerDeath;
        public static event Action<int> OnEnemyDestroyed;

        public static void TriggerPlayerDeath() => OnPlayerDeath?.Invoke();
        public static void TriggerEnemyDestroyed(int scoreValue)
            => OnEnemyDestroyed?.Invoke(scoreValue);
    }
}