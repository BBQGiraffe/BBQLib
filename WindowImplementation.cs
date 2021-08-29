using System.Numerics;
using System.Collections.Generic;
using System;
namespace BBQLib
{
    public abstract class WindowImplementation : IDrawableSurface, IDisposable
    {
        public abstract void Clear();
        public abstract void Draw(Sprite sprite);
        public abstract void Draw(string font, string text, Vector2 position);
        public abstract void Present();
        public abstract Sprite CreateSprite(string filename);
        public abstract void LoadFonts(string filename);
        public abstract void Dispose();
        public abstract bool IsKeyDown(KeyboardKey key);
        protected abstract void DrawSpriteInternal(Sprite sprite);

        protected readonly List<int> usedLayers = new List<int>();
        protected List<Sprite> sprites = new List<Sprite>();


        //I couldn't think of a better function name
        protected void Layer(int layer)
        {
            if(!usedLayers.Contains(layer))
            {
                usedLayers.Add(layer);
            }
        }

        protected void DrawSprites()
        {
            usedLayers.Sort();
            foreach(var sprite in sprites)
            {
                sprite.drawn = false;
            }
            foreach(var layer in usedLayers)
            {
                for(int i = 0; i < sprites.Count; i++)
                {
                    Sprite sprite = sprites[i];
                    if(!sprite.drawn)
                    {
                        if(sprite.layer == layer)
                        {
                            sprite.drawn = true;
                            DrawSpriteInternal(sprite);
                        }
                    }
                    
                }
            }
            usedLayers.Clear();
        }
        
        public abstract bool IsOpen
        {
            get;
        }

        public abstract float DeltaTime
        {
            get;
        }

        public abstract Vector2 Size
        {
            get;
        }
    }
}