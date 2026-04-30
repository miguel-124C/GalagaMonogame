using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Core.Events;

namespace Galaga.Managers
{
    public class HitsManager
    {
        private readonly EntityManager entityManager;
        public HitsManager(EntityManager em)
        {
            entityManager = em;
            EventManager.OnPlayerHitEnemy += HandlePlayerHitEnemy;
        }

        private void HandlePlayerHitEnemy(uint playerBullet, uint enemy)
        {
            var enemyHealth = entityManager.GetComponent<Health>(enemy);
            enemyHealth.Current -= 1;
            entityManager.AddComponent(enemy, enemyHealth);
            entityManager.AddComponent(playerBullet, new DestroyTag());
        }
    }
}