using System.Numerics;
using SFML.Window;
using System;
using SFML.System;
using SFML.Graphics;
namespace BBQLib
{
    namespace Backends
    {
        internal class SFMLWindow : WindowImplementation
        {

            public override Vector2 Size
            {
                get
                {
                    return new Vector2(window.Size.X, window.Size.Y);
                }
            }

            public override float DeltaTime
            {
                get
                {
                    return deltaTime;
                }
            }

            public SFMLWindow(WindowConfig config)
            {
                window = new RenderWindow(new VideoMode(config.width, config.height), config.name);
                window.SetFramerateLimit(60);
                window.Closed += (object o, EventArgs a) => window.Close();
            }

            public override bool IsOpen
            {
                get
                {
                    return window.IsOpen;
                }
            }

            float deltaTime;
            Clock deltaTImer = new Clock();


            private static RenderWindow window;

            
            
            public override void Clear()
            {
                window.DispatchEvents();
                window.Clear();
            }

            public override void Draw(Sprite sprite)
            {
                //sprite.Draw(this);
            }


            public override void Present()
            {
                window.Display();
            }
        }
    }
}