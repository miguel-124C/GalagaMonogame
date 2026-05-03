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
        private readonly List<uint> entityDeath = [];
        public override void Update(float deltaTime)
        {
            var healths = EntityManager.GetEntitiesWith<Health>();

            foreach (var entity in healths)
            {
                var health = EntityManager.GetComponent<Health>(entity);
                if (health.Current <= 0)
                {
                    // TODO: Refactor this code
                    var isPlayer = EntityManager.HasComponent<Player>(entity);
                    var isEnemy = EntityManager.HasComponent<Enemy>(entity);

                    string nameSprite = "Default_Explosion";
                    if (isPlayer)
                        nameSprite = "Player_Explosion";
                    else if (isEnemy)
                        nameSprite = "Enemy_Explosion";

                    entityExplosions.Add(entity, nameSprite);
                    entityDeath.Add(entity);
                    EntityManager.AddComponent(entity, new DestroyTag());
                }
            }

            HandleEntitiesDeath();
            CreateEntityExplosions();

            var destroyTags = EntityManager.GetEntitiesWith<DestroyTag>();
            foreach (var entity in destroyTags)
                EntityManager.DestroyEntity(entity);
        }

        private void CreateEntityExplosions()
        {
            foreach (var entity in entityExplosions)
            {
                var sprite = _spriteAtlas.GetSprite(entity.Value);
                sprite.TimePerFrame = 0.25f;

                var transformEntity = EntityManager.GetComponent<Transform>(entity.Key);
                var spriteEntity = EntityManager.GetComponent<Sprite>(entity.Key);
                var spriteEntityWidth = spriteEntity.SourceRectangle.Width * transformEntity.Scale.X;
                var spriteEntityHeight = spriteEntity.SourceRectangle.Height * transformEntity.Scale.Y;

                var transformExplosion = new Transform
                {
                    Position = new Vector2(
                        transformEntity.Position.X - (spriteEntityWidth / 2),
                        transformEntity.Position.Y - (spriteEntityHeight / 2)
                    ),
                    Scale = new Vector2(3, 3)
                };

                var explosion = EntityManager.CreateEntity();
                EntityManager.AddComponent(explosion, transformExplosion);
                EntityManager.AddComponent(explosion, new Explosion());
                EntityManager.AddComponent(explosion, sprite);
            }
            entityExplosions.Clear();
        }
        private void HandleEntitiesDeath()
        {
            foreach (var entity in entityDeath)
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
            }

            entityDeath.Clear();
        }
    }
}