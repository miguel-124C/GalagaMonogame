using Galaga.Components;
using Galaga.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Galaga.Managers
{
    public class SpriteAtlas(ContentManager cm, string pathJson)
    {
        private readonly ContentManager contentManager = cm;
        private readonly string jsonPath = pathJson;

        public Dictionary<string, Sprite> Sprites { get; private set; } = [];

        public void LoadJson()
        {
            string json = File.ReadAllText(jsonPath);
            var spriteAtlas = JsonSerializer.Deserialize<SpriteAtlasJson>(json);
            
            var texture = contentManager.Load<Texture2D>(spriteAtlas.TexturePath);

            var sprites = spriteAtlas.Sprites;
            foreach (var sprite in sprites)
            {
                Sprites[sprite.Name] = new Sprite
                {
                    Texture = texture,
                    SourceRectangle = new Rectangle((int)sprite.X, (int)sprite.Y, (int)sprite.W, (int)sprite.H),
                    TotalFrames = (int)sprite.TotalFrames,
                    CurrentFrame = 0,
                    SpriteEffect = SpriteEffects.None,
                    TimeElapsed = 0f
                };
            }
        }

        public Sprite GetSprite(string name)
        {
            if (Sprites.TryGetValue(name, out Sprite value))
                return value;

            throw new KeyNotFoundException($"Sprite with name '{name}' not found in the atlas.");
        }
    }
}