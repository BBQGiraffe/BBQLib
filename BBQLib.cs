using BBQLib.Backends;
using System.Collections.Generic;
namespace BBQLib
{
    public static class BBQLib
    {
        private static WindowImplementation window;   
        public static void Init(WindowConfig config, BackendType type = BackendType.SFML)
        {
            switch (type)
            {
                case BackendType.SFML:
                    window = new SFMLWindow(config);
                    break;
                case BackendType.SDL:
                    window = new SDLWindow(config);
                    break;
            }
        }

        public static Sprite RegisterSprite(string filename)
        {
            Sprite sprite = Json.Deserialize<Sprite>(filename);
            return sprite;
        }

        public static void Clear()
        {
            window.Clear();
        }

        public static bool IsOpen
        {
            get
            {
                return window.IsOpen;
            }
        }

        public static void Display()
        {
            window.Present();
        }
    }
}