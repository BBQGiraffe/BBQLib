using System.Numerics;
namespace BBQLib
{
    public interface IDrawableSurface
    {
        void Draw(Drawable drawable);
        void Clear();
        void Present();
        Vector2 Size{get;}
    }
}