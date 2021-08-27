using System.Numerics;
namespace BBQLib
{
    public sealed class Sprite
    {
        public Vector2 position = new Vector2();
        public Vector2 origin = new Vector2();
        public Vector2 scale = new Vector2(1,1);
        public bool repeat = false;
        public string name = "";
    }
}