using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Core.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Galaga.Systems
{
    public class PlayerControlSystem(GraphicsDeviceManager gdm)
        : ISystem
    {
        private readonly int WidthScreen = gdm.PreferredBackBufferWidth;
        private bool IsHitWallLeft = false;
        private bool IsHitWallRight = false;

        public override void Update(float deltaTime)
        {
            var player = EntityManager.GetEntitiesWith<Player>().FirstOrDefault();

            var hasPhysics = EntityManager.HasComponent<Physics>(player);
            var hasWeapon = EntityManager.HasComponent<Weapon>(player);
            var hasTransform = EntityManager.HasComponent<Transform>(player);
            if (!hasPhysics || !hasWeapon || !hasTransform) return;

            var physics = EntityManager.GetComponent<Physics>(player);
            var weapon = EntityManager.GetComponent<Weapon>(player);
            var transform = EntityManager.GetComponent<Transform>(player);
            var sprite = EntityManager.GetComponent<Sprite>(player);
            var spriteWidth = sprite.SourceRectangle.Width * transform.Scale.X;

            if (transform.Position.X <= 0)
            {
                transform.Position.X = 0;
                IsHitWallLeft = true;
                EntityManager.AddComponent(player, transform);
            }
            else if (transform.Position.X + spriteWidth >= WidthScreen)
            {
                transform.Position.X = WidthScreen - spriteWidth;
                IsHitWallRight = true;
                EntityManager.AddComponent(player, transform);
            }
            else
            {
                IsHitWallLeft = false;
                IsHitWallRight = false;
            }

            InputMove(player, physics);
            InputShoot(deltaTime, player, weapon, transform);
        }

        private void InputMove(uint entity, Physics physics)
        {
            var currentState = Keyboard.GetState();
            var isMovingHorizontal = currentState.IsKeyDown(Keys.Left)
                || currentState.IsKeyDown(Keys.Right);

            if (isMovingHorizontal)
            {
                if (currentState.IsKeyDown(Keys.Left))
                {
                    physics.Velocity.X = IsHitWallLeft ? 0 : -200;
                }
                else
                {
                    physics.Velocity.X = IsHitWallRight ? 0 : 200;
                }
            }
            else if (currentState.IsKeyUp(Keys.Right)
                || currentState.IsKeyUp(Keys.Left))
                physics.Velocity.X = 0;

            EntityManager.AddComponent(entity, physics);
        }

        private void InputShoot(
            float deltaTime, uint entity, Weapon weapon, Transform transform
        )
        {
            var currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Space))
            {
                weapon.CoolDown -= deltaTime;

                if (weapon.CoolDown <= 0)
                {
                    var spawnPosition = transform.Position + weapon.SpawnOffset;
                    weapon.CoolDown = weapon.FireRate;
                    EventManager.TriggerPlayerShoot(spawnPosition);
                }

                EntityManager.AddComponent(entity, weapon);
            }
        }
    }
}