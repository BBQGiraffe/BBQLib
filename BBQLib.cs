using BBQLib.Backends;
using System.Collections.Generic;
using System;
using System.Numerics;
namespace BBQLib
{
    public static class BBQLib
    {
        internal static WindowImplementation window;   
        public static string rootDirectory = "";
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
            return window.CreateSprite(filename);
        }

        public static void LoadFonts(string filename)
        {
            window.LoadFonts(filename);
        }

        public static void Clear()
        {
            window.Clear();
        }

        public static void Dispose()
        {

        }

        public static Vector2 Camera = new Vector2();

        public static Vector2 Size
        {
            get
            {
                return window.Size;
            }
        }
        
        public static void Draw(Sprite sprite)
        {
            window.Draw(sprite);
        }

        public static void Draw(string font, string text, Vector2 position)
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