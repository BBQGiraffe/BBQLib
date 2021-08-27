using System;
using System.Numerics;
using static SDL2.SDL;
namespace BBQLib
{
    namespace Backends
    {
        internal class SDLWindow : WindowImplementation
        {
            public override float DeltaTime => throw new System.NotImplementedException();

            public override Vector2 Size => throw new System.NotImplementedException();

            private bool open = true;

            public override bool IsOpen
            {
                get
                {
                    return open;
                }
            }

            private IntPtr window;
            private IntPtr renderer;
            public SDLWindow(WindowConfig config)
            {
                window = SDL_CreateWindow(config.name, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,(int)config.width, (int)config.height, SDL_WindowFlags.SDL_WINDOW_RESIZABLE | SDL_WindowFlags.SDL_WINDOW_MAXIMIZED);
                renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            }

            public override void Clear()
            {
                SDL_RenderClear(renderer);
                SDL_Delay(17);
            }

            public override void Present()
            {
                SDL_RenderPresent(renderer);
            }

            public override void Draw(Sprite sprite)
            {
                throw new NotImplementedException();
            }
        }
    }
}