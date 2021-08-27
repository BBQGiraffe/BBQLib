using System.Numerics;
namespace BBQLib
{
    public interface IDrawableSurface
    {
        void Draw(Sprite sprite);
        void Clear();
        void Present();
        Vector2 Size{get;}
    }
}