using System.Numerics;
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
            sprite.position = new Vector2();

            BBQLib.RegisterFont("font.json");
            Input.LoadAxisFile("axes.json");
            while(BBQLib.IsOpen)
            {
                BBQLib.Clear();
                
                Vector2 input = new Vector2(Input.GetAxis("horizontal"), -Input.GetAxis("vertical"));
                sprite.position += input * BBQLib.DeltaTime * 90;
                BBQLib.Draw(sprite);

                BBQLib.Draw("semen", string.Format("FPS:{0}", (int)(1f / BBQLib.DeltaTime)), new Vector2());
                BBQLib.Display();
            }
        }
    }
}
