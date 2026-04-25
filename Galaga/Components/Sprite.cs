using Galaga.Core.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaga.Components
{
    public struct Sprite : IComponent
    {
        public Texture2D Texture;
        public Rectangle SourceRectangle;
        public SpriteEffects SpriteEffect;
        public Color Tint;

        public int CurrentFrame, TotalFrames;
        public float TimePerFrame, TimeElapsed;
    }
}