using System.Numerics;
using System;
namespace BBQLib
{
    public abstract class WindowImplementation : IDrawableSurface, IDisposable
    {
        public abstract void Clear();
        public abstract void Draw(Sprite sprite);
        public abstract void Present();
        public abstract void RegisterSprite(Sprite sprite, string name);
        public abstract void Dispose();
        
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