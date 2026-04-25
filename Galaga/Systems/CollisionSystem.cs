using Galaga.Components;
using Galaga.Core.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Galaga.Systems
{
    public class CollisionSystem : ISystem
    {
        public static event Action<uint, uint> OnPlayerBulletHitEnemy;
        public static event Action<uint, uint> OnEnemyBulletHitPlayer;
        public static event Action<uint, uint> OnEnemyHitPlayer;

        public override void Update(float deltaTime)
        {
            var colliderEntities = EntityManager.GetEntitiesWith<Collider>();
            if (!colliderEntities.Any())
                return;

            foreach (var entity in colliderEntities)
            {
                if (!EntityManager.HasComponent<Transform>(entity))
                    continue;

                var playerEntities = EntityManager.GetEntitiesWith<Player>();
                var playerBulletEntities = EntityManager.GetEntitiesWith<PlayerBullet>();
                var enemyEntities = EntityManager.GetEntitiesWith<Enemy>();
                var enemyBulletEntities = EntityManager.GetEntitiesWith<EnemyBullet>();

                CheckCollision(playerBulletEntities, enemyEntities, OnPlayerBulletHitEnemy);
                CheckCollision(enemyBulletEntities, playerEntities, OnEnemyBulletHitPlayer);
                CheckCollision(enemyEntities, playerEntities, OnEnemyHitPlayer);
            }
        }

        private void CheckCollision
            (IEnumerable<uint> entityA, IEnumerable<uint> entityB, Action<uint, uint> onCollision)
        {
            if (!entityA.Any() || !entityB.Any()) return;

            foreach (var a in entityA)
            {
                var rectA = GetRectEntity(a);

                foreach (var b in entityB)
                {
                    var rectB = GetRectEntity(b);
                    
                    if (rectA.Intersects(rectB))
                        onCollision?.Invoke(a, b);
                }
            }
        }

        private Rectangle GetRectEntity(uint entity)
        {
            var transform = EntityManager.GetComponent<Transform>(entity);
            var collider = EntityManager.GetComponent<Collider>(entity);

            return new(
                (int)(transform.Position.X + collider.Offset.X),
                (int)(transform.Position.Y + collider.Offset.Y),
                collider.Width, collider.Height
            );
        }
    }
}