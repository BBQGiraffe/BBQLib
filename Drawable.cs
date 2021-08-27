using System.Numerics;
namespace BBQLib
{
    public abstract class Drawable
    {
        public abstract void Draw(IDrawableSurface surface);

        public Vector2 position = new Vector2();
        public float rotation;
        public int layer;
    }
}