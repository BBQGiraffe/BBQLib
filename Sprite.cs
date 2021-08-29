using System.Numerics;
using Newtonsoft.Json;
namespace BBQLib
{
    public sealed class Sprite
    {
        public Vector2 position = new Vector2();
        public Vector2 origin = new Vector2();
        public Vector2 scale = new Vector2(1,1);
        public float rotation;
        public int layer = 0;
        public bool drawn = false;
        public bool repeat = false;
        public string textureFile = "";

        [JsonIgnore]
        public string json;

        [JsonIgnore]
        public Vector2 size = new Vector2();

        [JsonIgnore]
        public BoundingBox bounds
        {
            get
            {
                return GetBounds();
            }
        }

        public BoundingBox GetBounds()
        {
            BoundingBox output = new BoundingBox(new Vector2(size.X, size.Y) * 1.5f);
            output.min =  position - (size / 1.5f);
            return output;
        }
    }
}