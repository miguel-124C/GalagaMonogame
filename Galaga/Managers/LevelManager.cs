using Galaga.Core.Events;
using Galaga.Factories;
using Galaga.Helpers;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Galaga.Managers
{
    public class LevelManager
    {
        public int CurrentLevel { get; private set; } = 1;
        public int CurrentScore { get; private set; } = 0;
        public int EnemiesAlive { get; private set; } = 0;

        private readonly EnemyFactory _enemyFactory;

        public LevelManager(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            EventManager.OnEnemyDeath += CheckEnemies;
        }

        private void CheckEnemies(int score)
        {
            CurrentScore += score;
            EnemiesAlive -= 1;

            if (EnemiesAlive <= 0)
            {
                CurrentLevel += 1;
                StartNextWave();
            }
        }

        public void StartNextWave()
        {
            //_enemyFactory.CreateButterfly();
            var amountButterfly = 4;
            var amountBee = 4;
            var butterflyPoints = Constants.PointsSubWaveOne.ToList().ElementAt(0).Points;
            var beePoints = Constants.PointsSubWaveOne.ToList().ElementAt(1).Points;

            var gap = Constants.EnemiesGap;
            var initialPositionY = -100;

            for (int i = 0; i < amountButterfly; i++)
            {
                _enemyFactory.CreateButterfly(
                    new Vector2(200, initialPositionY),
                    butterflyPoints
                );

                initialPositionY -= gap;
                EnemiesAlive += 1;
            }

            for (int i = 0; i < amountBee; i++)
            {
                _enemyFactory.CreateBee(new Vector2(400, -100), beePoints);
                EnemiesAlive += 1;
            }
        }
    }
}