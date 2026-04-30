using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Core.Events;
using Galaga.Managers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Galaga.Systems
{
    public class DeathSystem(SpriteAtlas spriteAtlas) : ISystem
    {
        private readonly SpriteAtlas _spriteAtlas = spriteAtlas;
        private readonly Dictionary<uint, string> entityExplosions = [];
        public override void Update(float deltaTime)
        {
            var healths = EntityManager.GetEntitiesWith<Health>();

            foreach (var entity in healths)
            {
                var health = EntityManager.GetComponent<Health>(entity);
                if (health.Current <= 0)
                {
                    var isPlayer = EntityManager.HasComponent<Player>(entity);
                    var isEnemy = EntityManager.HasComponent<Enemy>(entity);

                    if (isPlayer)
                        EventManager.TriggerPlayerDeath();
                    if (isEnemy)
                    {
                        var score = EntityManager.GetComponent<ScoreValue>(entity);
                        EventManager.TriggerEnemyDeath(score.Value);
                    }

                    string nameSprite = "Default_Explosion";
                    if (isPlayer)
                        nameSprite = "Player_Explosion";
                    else if (isEnemy)
                        nameSprite = "Enemy_Explosion";

                    entityExplosions.Add(entity, nameSprite);
                    EntityManager.AddComponent(entity, new DestroyTag());
                }
            }

            CreateEntityExplosions();

            var destroyTags = EntityManager.GetEntitiesWith<DestroyTag>();
            foreach (var entity in destroyTags)
                EntityManager.DestroyEntity(entity);
        }

        private void CreateEntityExplosions()
        {
            foreach (var entity in entityExplosions)
            {
                var transformEntity = EntityManager.GetComponent<Transform>(entity.Key);
                var transformExplosion = new Transform
                {
                    Position = transformEntity.Position,
                    Scale = new Vector2(3, 3)
                };

                var sprite = _spriteAtlas.GetSprite(entity.Value);
                sprite.TimePerFrame = 0.15f;

                var explosion = EntityManager.CreateEntity();
                EntityManager.AddComponent(explosion, transformExplosion);
                EntityManager.AddComponent(explosion, new Explosion());
                EntityManager.AddComponent(explosion, sprite);
            }
            entityExplosions.Clear();
        }
    }
}