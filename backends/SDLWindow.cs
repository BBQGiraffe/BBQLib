using System;
using System.Numerics;
using System.Collections.Generic;
using static SDL2.SDL;
using static SDL2.SDL_image;
using static SDL2.SDL_ttf;
namespace BBQLib
{
    namespace Backends
    {
        internal class SDLWindow : WindowImplementation, IDisposable
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
                SDL_RenderSetLogicalSize(renderer, (int)config.width, (int)config.height);
            }

            uint tickCount;

            SDL_Event sdlEvent;

            void PollEvents()
            {
                while(SDL_PollEvent(out sdlEvent) > 0){
                    switch (sdlEvent.type)
                    {
                        case SDL_EventType.SDL_QUIT:
                            open = false;
                            break;
                    }
                }
            }

            public override void Clear()
            {
                PollEvents();
                deltaTime = (SDL_GetTicks() - tickCount) / 1000.0f;
                tickCount = SDL_GetTicks();
                SDL_RenderClear(renderer);
                SDL_Delay(17);
            }

            static Dictionary<string, IntPtr> textures = new Dictionary<string, IntPtr>();
            static Dictionary<string, IntPtr> fonts = new Dictionary<string, IntPtr>();

            public override void RegisterFont(Font font, string name)
            {
                var sdlFont = TTF_OpenFont(font.ttf, (int)font.size);
                fonts.Add(font.name, sdlFont);
            }

            public override void RegisterSprite(Sprite sprite, string name)
            {
                IntPtr texture = IMG_LoadTexture(renderer, sprite.name);
                int w, h, asdf;
                uint asdfasdf;
                SDL_QueryTexture(texture, out asdfasdf, out asdf, out w, out h);
                sprite.size = new Vector2(w, h);
                textures.Add(name, texture);
            }

            public override void Present()
            {
                SDL_RenderPresent(renderer);
            }

            public override void Draw(Sprite sprite)
            {
                IntPtr texture = textures[sprite.json];
                DrawSDLTexture(sprite.position, texture, sprite.scale, sprite.rotation, sprite.origin);
            }

            void DrawSDLTexture(Vector2 position, IntPtr texture, Vector2 scale, float rotation, Vector2 origin)
            {

                int width, height, cum;
                uint semen;
                SDL_QueryTexture(texture, out semen, out cum, out width, out height);

                SDL_Rect src = new SDL_Rect()
                {
                    x = 0,
                    y = 0,
                    w =  width,
                    h = height
                };

                SDL_Point center = new SDL_Point()
                {
                    x = (int)origin.X,
                    y = (int)origin.Y
                };


                //SDL_RenderCopyExF was REFUSING to work properly so I guess
                //if you want to use SDL2 you're stuck with integer sprite positions
                SDL_Rect rect = new SDL_Rect()
                {
                    x = (int)position.X,
                    y = (int)position.Y,
                    w = width * (int)scale.X,
                    h = width * (int)scale.Y
                };

                SDL_RenderCopyEx(renderer, texture, ref src, ref rect, rotation, ref center, SDL_RendererFlip.SDL_FLIP_NONE);
            }

            public override void Draw(Font font, string text, Vector2 position)
            {
                var sdlFont = fonts[font.name];
                SDL_Color color = new SDL_Color()
                {
                    r = 255,
                    g = 255,
                    b = 255
                };

                var surface = TTF_RenderText_Solid(sdlFont, text, color);
                IntPtr texture = SDL_CreateTextureFromSurface(renderer, surface);
                DrawSDLTexture(position, texture, new Vector2(1,1), 0, new Vector2());
            }

            public override void Dispose()
            {
                SDL_DestroyRenderer(renderer);
                SDL_DestroyWindow(window);
                foreach(var texture in textures)
                {
                    SDL_DestroyTexture(texture.Value);
                }
            }
        }
    }
}