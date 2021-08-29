using System.Numerics;
namespace BBQLib
{
    public interface IDrawableSurface
    {
        void Clear();
        void Present();
        Vector2 Size{get;}
    }
}