using System;
using System.Numerics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
                Console.WriteLine("checking if SDL2 is working... {0}", SDL_Init(SDL_INIT_EVERYTHING) > -1);
                Console.WriteLine("checking if SDL2 TTF is working... {0}", TTF_Init() > -1);
                Console.WriteLine("checking if SDL2 Image is working... {0}", IMG_Init(IMG_InitFlags.IMG_INIT_PNG) > -1);
                window = SDL_CreateWindow(config.name, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,(int)config.width, (int)config.height, SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
                renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
                Console.WriteLine("setting logical render size... {0}", SDL_RenderSetLogicalSize(renderer, (int)config.width, (int)config.height) > -1);
                Console.WriteLine("mapping SDL2 keys....");
                MapSDLKeys();
            }

            public 

            uint tickCount;

            SDL_Event sdlEvent;

            public override bool IsKeyDown(KeyboardKey key)
            {
                return keyState[(int)sdlKeys[key]] > 0;
            }

            byte[] keyState;

            void PollEvents()
            {
                int keyCount = 0;
                IntPtr keys = SDL_GetKeyboardState(out keyCount);
                keyState = new byte[keyCount];

                Marshal.Copy(keys, keyState, 0, keyCount);
                Input.ProcessInputs();

                while(SDL_PollEvent(out sdlEvent) > 0){
                    switch (sdlEvent.type)
                    {
                        case SDL_EventType.SDL_QUIT:
                            open = false;
                            break;
                    }
                }
            }

            Dictionary<KeyboardKey, SDL_Scancode> sdlKeys = new Dictionary<KeyboardKey, SDL_Scancode>();

            void MapSDLKeys()
            {
                sdlKeys.Add(KeyboardKey.Q, SDL_Scancode.SDL_SCANCODE_Q);
                sdlKeys.Add(KeyboardKey.W, SDL_Scancode.SDL_SCANCODE_W);
                sdlKeys.Add(KeyboardKey.E, SDL_Scancode.SDL_SCANCODE_E);
                sdlKeys.Add(KeyboardKey.R, SDL_Scancode.SDL_SCANCODE_R);
                sdlKeys.Add(KeyboardKey.T, SDL_Scancode.SDL_SCANCODE_T);
                sdlKeys.Add(KeyboardKey.Y, SDL_Scancode.SDL_SCANCODE_Y);
                sdlKeys.Add(KeyboardKey.U, SDL_Scancode.SDL_SCANCODE_U);
                sdlKeys.Add(KeyboardKey.I, SDL_Scancode.SDL_SCANCODE_I);
                sdlKeys.Add(KeyboardKey.O, SDL_Scancode.SDL_SCANCODE_O);
                sdlKeys.Add(KeyboardKey.P, SDL_Scancode.SDL_SCANCODE_P);
                sdlKeys.Add(KeyboardKey.A, SDL_Scancode.SDL_SCANCODE_A);
                sdlKeys.Add(KeyboardKey.S, SDL_Scancode.SDL_SCANCODE_S);
                sdlKeys.Add(KeyboardKey.D, SDL_Scancode.SDL_SCANCODE_D);
                


                sdlKeys.Add(KeyboardKey.Up, SDL_Scancode.SDL_SCANCODE_UP);
                sdlKeys.Add(KeyboardKey.Down, SDL_Scancode.SDL_SCANCODE_DOWN);
                sdlKeys.Add(KeyboardKey.Left, SDL_Scancode.SDL_SCANCODE_LEFT);
                
            }

            public override void Clear()
            {
                sprites.Clear();
                PollEvents();
                deltaTime = (SDL_GetTicks() - tickCount) / 1000.0f;
                tickCount = SDL_GetTicks();
                SDL_RenderClear(renderer);
                SDL_Delay(17);
            }

            static Dictionary<string, IntPtr> textures = new Dictionary<string, IntPtr>();
            static Dictionary<string, IntPtr> fonts = new Dictionary<string, IntPtr>();

            public override void RegisterFont(Font font)
            {
                var sdlFont = TTF_OpenFont(font.ttf, (int)font.size);
                fonts.Add(font.name, sdlFont);
            }

            public override Sprite CreateSprite(string filename)
            {
                var sprite = Json.Deserialize<Sprite>(filename);
                sprite.json = filename;
                IntPtr texture;
                if(!textures.ContainsKey(filename))
                {
                    texture = IMG_LoadTexture(renderer, BBQLib.rootDirectory + sprite.textureFile);
                    textures.Add(filename, texture);
                }
                texture = textures[filename];
                int w, h, asdf;
                uint asdfasdf;
                SDL_QueryTexture(texture, out asdfasdf, out asdf, out w, out h);
                sprite.size = new Vector2(w, h);
                return sprite;
            }
        
            public override void Present()
            {
                DrawSprites();
                SDL_RenderPresent(renderer);
            }

            public override void Draw(Sprite sprite)
            {
                sprites.Add(sprite);
                Layer(sprite.layer);
                sprite.drawn = false;
            }

            protected override void DrawSpriteInternal(Sprite sprite)
            {
                IntPtr texture = textures[sprite.json];
                DrawSDLTexture(sprite.position - BBQLib.Camera, texture, sprite.scale, sprite.rotation, sprite.origin);
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
                    x = (int)position.X - (int)origin.X,
                    y = (int)position.Y - (int)origin.Y, 
                    w = width * (int)scale.X,
                    h = height * (int)scale.Y
                };

                SDL_RenderCopyEx(renderer, texture, ref src, ref rect, rotation, ref center, SDL_RendererFlip.SDL_FLIP_NONE);
            }

            public override void Draw(string fontName, string text, Vector2 position)
            {
                var sdlFont = fonts[fontName];
                SDL_Color color = new SDL_Color()
                {
                    r = 255,
                    g = 255,
                    b = 255,
                    a = 255
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