using Microsoft.Xna.Framework;
using System;

namespace Galaga.Core.Events
{
    public static class EventManager
    {
        public static event Action OnPlayerDeath;
        public static event Action<int> OnEnemyDeath;
        public static event Action<Vector2> OnPlayerShoot;
        public static event Action<Vector2> OnEnemyShoot;

        public static event Action<uint, uint> OnPlayerHitEnemy;
        public static event Action<uint, uint> OnEnemyHitPlayer;
        public static event Action<uint, uint> OnEnemyDestroyPlayer;

        public static void TriggerPlayerDeath() => OnPlayerDeath?.Invoke();
        public static void TriggerEnemyDeath(int scoreValue)
            => OnEnemyDeath?.Invoke(scoreValue);

        public static void TriggerPlayerShoot(Vector2 position)
            => OnPlayerShoot?.Invoke(position);

        public static void TriggerEnemyShoot(Vector2 position)
            => OnEnemyShoot?.Invoke(position);

        public static void TriggerPlayerHitEnemy(uint bullet, uint enemy)
            => OnPlayerHitEnemy?.Invoke(bullet, enemy);

        public static void TriggerEnemyHitPlayer(uint bullet, uint player)
            => OnEnemyHitPlayer?.Invoke(bullet, player);

        public static void TriggerEnemyDestroyPlayer(uint enemy, uint player)
        {
            OnEnemyDestroyPlayer?.Invoke(enemy, player);
            TriggerEnemyDeath(0);
            TriggerPlayerDeath();
        }
    }
}