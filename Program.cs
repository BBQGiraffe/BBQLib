using System;

namespace BBQLib
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowConfig config = new WindowConfig()
            {
                name = "Window Test",
                width = 640,
                height = 480,
                fps = 60
            };


            BBQLib.Init(config, BackendType.SFML);
            
            Sprite sprite = BBQLib.RegisterSprite("testsprite.json");
            Font font = BBQLib.RegisterFont("font.json");
            
            while(BBQLib.IsOpen)
            {
                BBQLib.Clear();
                BBQLib.Draw(sprite);
                BBQLib.Draw(font, string.Format("FPS:{0}", (int)(1f / BBQLib.DeltaTime)), new System.Numerics.Vector2());
                sprite.rotation += 180 * BBQLib.DeltaTime;
                BBQLib.Display();
            }
        }
    }
}
