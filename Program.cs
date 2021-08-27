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


            //SDL2 backend doesn't work a lot of times on Linux
            BBQLib.Init(config, BackendType.SFML);

            while(BBQLib.IsOpen)
            {
                BBQLib.Clear();

                BBQLib.Display();
            }
        }
    }
}
