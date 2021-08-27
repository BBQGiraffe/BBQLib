using System.Numerics;
namespace BBQLib
{
    public abstract class WindowImplementation : IDrawableSurface
    {
        public abstract void Clear();
        public abstract void Draw(Drawable drawable);
        public abstract void Present();

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