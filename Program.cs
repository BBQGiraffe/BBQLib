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
            
            while(BBQLib.IsOpen)
            {
                BBQLib.Clear();
                BBQLib.Draw(sprite);
                sprite.rotation += 180 * BBQLib.DeltaTime;
                BBQLib.Display();
            }
        }
    }
}
