using Galaga.Core.ECS;
using Galaga.Factories;
using Galaga.Managers;
using Galaga.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaga
{
    public class GalagaGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private EntityManager _entityManager;
        private SpriteAtlas _spriteAtlas;
        private readonly SystemsManager _systemsManager;
        private readonly AudioManager _audioManager;
        private LevelManager _levelManager;

        private RenderSystem _renderSystem;

        public GalagaGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1024;   // Ancho
            _graphics.PreferredBackBufferHeight = 768;   // Alto
            _systemsManager = new SystemsManager();
            _audioManager = new AudioManager("soundEffects");
        }

        protected override void Initialize()
        {
            _entityManager = new EntityManager();
            _spriteAtlas = new SpriteAtlas(Content, "Content/galaga_atlas.json");

            _systemsManager.RegisterSystem(new PlayerControlSystem(_graphics));
            _systemsManager.RegisterSystem(new EnemyAISystem(_spriteAtlas, _graphics));
            _systemsManager.RegisterSystem(new MovementSystem());
            _systemsManager.RegisterSystem(new CollisionSystem());
            _systemsManager.RegisterSystem(new BulletSystem(_graphics));
            _systemsManager.RegisterSystem(new AnimationSystem());
            _systemsManager.RegisterSystem(new DeathSystem(_spriteAtlas));

            var bulletFactory = new BulletFactory(_entityManager, _spriteAtlas);
            _ = new BulletManager(bulletFactory);
            _ = new HitsManager(_entityManager);

            EnemyFactory enemyFactory = new(_entityManager, _spriteAtlas);
            _levelManager = new LevelManager(enemyFactory);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _renderSystem = new RenderSystem(_spriteBatch);
            _renderSystem.Initialize(_entityManager);
            _systemsManager.InitializeSystems(_entityManager);

            _spriteAtlas.LoadJson();
            _audioManager.LoadSounds(Content);

            EntityFactory ef = new(_entityManager, _spriteAtlas);
            ef.CreatePlayer(new Vector2(400, 700));
            _levelManager.StartNextWave();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _systemsManager.UpdateSystems(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _renderSystem.Update(deltaTime);

            base.Draw(gameTime);
        }
    }
}