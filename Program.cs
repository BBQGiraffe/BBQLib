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
                width = 800,
                height = 600
            };


            BBQLib.Init(config, BackendType.SDL);
            
            Sprite sprite = BBQLib.RegisterSprite("testsprite.json");
            Font font = BBQLib.RegisterFont("font.json");
            
            while(BBQLib.IsOpen)
            {
                BBQLib.Clear();
                BBQLib.Draw(sprite);
                BBQLib.Draw(font, string.Format("FPS:{0}", 1f / BBQLib.DeltaTime), new System.Numerics.Vector2());
                sprite.rotation += 180 * BBQLib.DeltaTime;
                BBQLib.Display();
            }
        }
    }
}
