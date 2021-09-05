using BBQLib.Backends;
using System.Collections.Generic;
using System;
using System.IO;
using System.Numerics;
namespace BBQLib
{
    public static class BBQLib
    {
        internal static WindowImplementation window;   
        internal static SoundPlayer soundPlayer;
        public static string rootDirectory = "";
        public static void Init(WindowConfig config)
        {
            window = new SFMLWindow(config);
            soundPlayer = new SFMLSoundPlayer();
        }
        public static float DeltaTime
        {
            get
            {
                return window.DeltaTime;
            }
        }

        public static void PlaySound(string sound)
        {
            soundPlayer.PlaySound(sound);
        }

        public static void LoadSounds(string directory)
        {
            soundPlayer.soundDir = rootDirectory + directory;
            foreach(var filename in Directory.GetFiles(rootDirectory + directory))
            {
                soundPlayer.LoadSound(filename);
            }
            
        }

        public static Sprite CreateSprite(byte[] palette, byte[] buffer, uint width, uint height, string name)
        {
            return PalettedSprite.CreatePalettedSprite(palette, buffer, width, height, false, name);
        }

        public static Sprite CreateSprite(byte[] buffer, uint width, uint height, string name)
        {
            return window.CreateSprite(buffer, width, height, name);
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

        public static Vector2 ScreenToWorld(Vector2 A)
        {
            return A + Camera - window.Size / 2;
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

        public static void Line(Vector2 A, Vector2 B, Color color)
        {
            window.DrawLine(A, B, 4, color);
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