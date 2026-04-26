using Galaga.Components;
using Galaga.Core.ECS;
using Galaga.Core.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Galaga.Systems
{
    public class PlayerControlSystem(EntityManager em, GraphicsDeviceManager gdm)
        : ISystem
    {
        private readonly EntityManager _entityManager = em;
        private readonly int WidthScreen = gdm.PreferredBackBufferWidth;

        private KeyboardState _currentKeyboardState = Keyboard.GetState();
        private KeyboardState _previousKeyboardState = Keyboard.GetState();

        public override void Update(float deltaTime)
        {
            var player = _entityManager.GetEntitiesWith<Player>().FirstOrDefault();

            var hasPhysics = _entityManager.HasComponent<Physics>(player);
            var hasWeapon = _entityManager.HasComponent<Weapon>(player);
            var hasTransform = _entityManager.HasComponent<Transform>(player);
            if (!hasPhysics || !hasWeapon || !hasTransform) return;

            var physics = _entityManager.GetComponent<Physics>(player);
            var weapon = _entityManager.GetComponent<Weapon>(player);
            var transform = _entityManager.GetComponent<Transform>(player);

            if (transform.Position.X <= 0)
                transform.Position.X = 0;

            if (transform.Position.X >= WidthScreen)
                transform.Position.X = WidthScreen;

            _currentKeyboardState = Keyboard.GetState();
            InputMove(physics);
            InputShoot(deltaTime, player, weapon, transform);

            _previousKeyboardState = _currentKeyboardState;
        }

        private void InputMove(Physics physics)
        {
            if (IsKeyPressed(Keys.Left, _currentKeyboardState))
                physics.Velocity.X = -200;
            else if (IsKeyPressed(Keys.Right, _currentKeyboardState))
                physics.Velocity.X = 200;
            else if (_currentKeyboardState.IsKeyUp(Keys.Right)
                || _currentKeyboardState.IsKeyUp(Keys.Left))
                physics.Velocity.X = 0;
        }

        private void InputShoot(
            float deltaTime, uint entity, Weapon weapon, Transform transform
        )
        {
            if (IsKeyPressed(Keys.Space, _currentKeyboardState))
            {
                weapon.CoolDown -= deltaTime;
                _entityManager.AddComponent(entity, weapon);

                var spawnPosition = transform.Position + weapon.SpawnOffset;

                if (weapon.CoolDown <= 0)
                {
                    weapon.CoolDown = weapon.FireRate;
                    EventManager.TriggerPlayerShoot(spawnPosition);
                }
            }
        }

        private bool IsKeyPressed(Keys key, KeyboardState currentState)
            => currentState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    }
}