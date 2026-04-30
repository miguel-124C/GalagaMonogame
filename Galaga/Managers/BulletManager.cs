using Galaga.Core.Events;
using Galaga.Factories;
using Microsoft.Xna.Framework;

using Galaga.Interfaces;

namespace Galaga.Managers
{
    public class BulletManager
    {
        private readonly BulletFactory _bulletFactory;

        public BulletManager(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
            EventManager.OnPlayerShoot += HandlePlayerShoot;
            EventManager.OnEnemyShoot += HandleEnemyShoot;
        }

        private void HandlePlayerShoot(Vector2 position)
        {
            _bulletFactory.CreateBullet(position, BulletType.PlayerBullet);
        }

        private void HandleEnemyShoot(Vector2 position)
        {
            _bulletFactory.CreateBullet(position, BulletType.EnemyBullet);
        }
    }
}