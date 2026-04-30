using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Core.Events;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Galaga.Systems
{
    public class CollisionSystem : ISystem
    {
        public override void Update(float deltaTime)
        {
            var players = EntityManager.GetEntitiesWith<Player>();
            var playerBullets = EntityManager.GetEntitiesWith<PlayerBullet>();
            var enemies = EntityManager.GetEntitiesWith<Enemy>();
            var enemyBullets = EntityManager.GetEntitiesWith<EnemyBullet>();

            var playerEntities = players.Where(HasColliderAndTransform);
            var playerBulletEntities = playerBullets.Where(HasColliderAndTransform);
            var enemyEntities = enemies.Where(HasColliderAndTransform);
            var enemyBulletEntities = enemyBullets.Where(HasColliderAndTransform);

            if (playerBulletEntities.Any() && enemyEntities.Any())
                CheckCollision(playerBulletEntities, enemyEntities, EventManager.TriggerPlayerHitEnemy);

            if (enemyBulletEntities.Any() && playerEntities.Any())
                CheckCollision(enemyBulletEntities, playerEntities, EventManager.TriggerEnemyHitPlayer);

            if (enemyEntities.Any() && playerEntities.Any())
                CheckCollision(enemyEntities, playerEntities, EventManager.TriggerEnemyDestroyPlayer);
        }

        private void CheckCollision
            (IEnumerable<uint> entityA, IEnumerable<uint> entityB, Action<uint, uint> onCollision)
        {
            foreach (var a in entityA)
            {
                var rectA = GetRectEntity(a);

                foreach (var b in entityB)
                {
                    var rectB = GetRectEntity(b);
                    
                    if (rectA.Intersects(rectB))
                        onCollision(a, b);
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

        private bool HasColliderAndTransform(uint entity)
        {
            return EntityManager.HasComponent<Transform>(entity) &&
                EntityManager.HasComponent<Collider>(entity);
        }
    }
}