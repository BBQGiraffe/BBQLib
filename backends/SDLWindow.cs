using System;
using System.Numerics;
using System.Collections.Generic;
using static SDL2.SDL;
using static SDL2.SDL_image;
namespace BBQLib
{
    namespace Backends
    {
        internal class SDLWindow : WindowImplementation
        {
            public override float DeltaTime 
            {
                get
                {
                    return deltaTime;
                }
            }

            float deltaTime;

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
                SDL_Init(SDL_INIT_EVERYTHING);
                IMG_Init(IMG_InitFlags.IMG_INIT_PNG);
                window = SDL_CreateWindow(config.name, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,(int)config.width, (int)config.height, SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
                renderer = SDL_CreateRenderer(window, -1,0);
            }


            uint tickCount;
            public override void Clear()
            {
                deltaTime = (SDL_GetTicks() - tickCount) / 1000.0f;
                tickCount = SDL_GetTicks();

                SDL_RenderClear(renderer);
                SDL_Delay(17);
            }

            static Dictionary<string, IntPtr> textures = new Dictionary<string, IntPtr>();
            public override void RegisterSprite(Sprite sprite, string name)
            {
                IntPtr texture = IMG_LoadTexture(renderer, sprite.name);
                int w, h, asdf;
                uint asdfasdf;
                SDL_QueryTexture(texture, out asdfasdf, out asdf, out w, out h);
                sprite.size = new Vector2(w, h);
                Console.WriteLine(sprite.size);
                textures.Add(name, texture);
            }

            public override void Present()
            {
                SDL_RenderPresent(renderer);
            }

            public override void Draw(Sprite sprite)
            {
                SDL_Point center = new SDL_Point()
                {
                    x = (int)sprite.origin.X,
                    y = (int)sprite.origin.Y
                };

                SDL_Rect src = new SDL_Rect()
                {
                    x = 0,
                    y = 0,
                    w = (int)sprite.size.X,
                    h = (int)sprite.size.Y
                };


                //SDL_RenderCopyExF was REFUSING to work properly so I guess
                //if you want to use SDL2 you're stuck with integer sprite positions
                SDL_Rect rect = new SDL_Rect()
                {
                    x = (int)sprite.position.X,
                    y = (int)sprite.position.Y,
                    w = (int)sprite.size.X * (int)sprite.scale.X,
                    h = (int)sprite.size.Y * (int)sprite.scale.Y
                };
                IntPtr texture = textures[sprite.json];
                SDL_RenderCopyEx(renderer, texture, ref src, ref rect, sprite.rotation, ref center, SDL_RendererFlip.SDL_FLIP_NONE);

            }
        }
    }
}