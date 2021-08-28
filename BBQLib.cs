using BBQLib.Backends;
using System.Collections.Generic;
using System;
using System.Numerics;
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
        public static float DeltaTime
        {
            get
            {
                return window.DeltaTime;
            }
        }
        public static Sprite RegisterSprite(string filename)
        {
            Sprite sprite = Json.Deserialize<Sprite>(filename);
            sprite.json = filename;
            window.RegisterSprite(sprite, filename);
            return sprite;
        }

        public static Font RegisterFont(string filename)
        {
            Font font = Json.Deserialize<Font>(filename);
            font.jsonFilename = filename;
            window.RegisterFont(font);
            return font;
        }

        public static void Clear()
        {
            window.Clear();
        }

        public static void Dispose()
        {

        }

        
        public static void Draw(Sprite sprite)
        {
            window.Draw(sprite);
        }

        public static void Draw(Font font, string text, Vector2 position)
        {
            window.Draw(font, text, position);
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