using System.Numerics;
using SFML.Window;
using System;
using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;
namespace BBQLib
{
    namespace Backends
    {
        internal class SFMLWindow : WindowImplementation, IDisposable
        {
            public override void Dispose()
            {
                window.Dispose();
                foreach(var sprite in sfSprites.Values)
                {
                    sprite.Dispose();
                }
            }

            public override Vector2 Size
            {
                get
                {
                    return windowSize;
                }
            }

            private Vector2 windowSize = new Vector2();

            public override float DeltaTime
            {
                get
                {
                    return deltaTime;
                }
            }

            Dictionary<string, SFML.Graphics.Sprite> sfSprites = new Dictionary<string, SFML.Graphics.Sprite>();
            Dictionary<string, SFML.Graphics.Text> sfTexts = new Dictionary<string, Text>();
            float deltaTime;
            readonly Clock deltaTimer = new Clock();
            static RenderWindow window;


            public SFMLWindow(WindowConfig config)
            {
                window = new RenderWindow(new VideoMode(config.width, config.height), config.name);
                window.SetFramerateLimit(config.fps);
                windowSize = new Vector2(config.width, config.height);
                window.Closed += (object o, EventArgs a) => window.Close();
                aspectRatio = ((float)config.width / (float)config.height);
                window.Resized += (sender, e) => PreserveAspectRatio();
            }

            public override bool IsOpen
            {
                get
                {
                    return window.IsOpen;
                }
            }
            

            public override void Clear()
            {
                Input.ProcessInputs();
                sprites.Clear();
                usedLayers.Clear();
                window.DispatchEvents();
                window.Clear();
                deltaTime = deltaTimer.Restart().AsSeconds();
            }

            public override Sprite CreateSprite(byte[] frameBuffer, uint width, uint height, string name)
            {
                SFML.Graphics.Image image = new SFML.Graphics.Image(width, height, frameBuffer);
                SFML.Graphics.Sprite sfSprite = new SFML.Graphics.Sprite(new SFML.Graphics.Texture(image));

                Sprite sprite = new Sprite();
                sprite.size = new Vector2(width, height);
                sprite.json = name;
                sprite.textureFile = "SFML_GENERATEDTEXTURE_" + name;
                sfSprites.Add(name, sfSprite);
                return sprite;

            }
        
            public override Sprite CreateSprite(string filename)
            {
                var sprite = Json.Deserialize<Sprite>(filename);
                sprite.json = filename;
                if(!sfSprites.ContainsKey(filename))
                {
                    var sfSprite = new SFML.Graphics.Sprite(new Texture(BBQLib.rootDirectory + sprite.textureFile));
                    sfSprites.Add(filename, sfSprite);
                }
                return sprite;
            }

            public override bool IsKeyDown(KeyboardKey key)
            {
                return Keyboard.IsKeyPressed((Keyboard.Key)key);
            }

            public override Vector2 GetMouse()
            {
                var position = SFML.Window.Mouse.GetPosition(window);
                return new Vector2(position.X, position.Y);
            }

            public override void Draw(Sprite sprite)
            {
                sprite.drawn = false;
                Layer(sprite.layer);
                sprites.Add(sprite);
            }

            public override void Present()
            {
                
                DrawSprites();
                window.Display();
            }

            public override void Draw(string font, string text, Vector2 position)
            {
                var sfText = sfTexts[font];
                sfText.Position = new Vector2f(position.X, position.Y);
                sfText.DisplayedString = text;
                window.Draw(sfText);
            }

            public override void LoadFonts(string filename)
            {
                var fonts = Json.Deserialize<Dictionary<string, Font>>(filename);
                foreach(var font in fonts)
                {
                    var sfFont = new SFML.Graphics.Font(BBQLib.rootDirectory + font.Value.ttf);
                    var sfText = new Text("Example Text", sfFont, font.Value.size);
                    sfTexts.Add(font.Key, sfText);
                }
            }

            //sfml doesn't have built in aspect preservation, stole this code from OpenNitemare3D lol
            static float aspectRatio = 1.33333333f;
            static void PreserveAspectRatio()//mm letterboxing
            {
                var m_window_width = window.Size.X;
                var m_window_height = window.Size.Y;
                float new_width = aspectRatio * m_window_height;
                float new_height = m_window_width / aspectRatio;
                float offset_width = (m_window_width - new_width) / 2.0f;
                float offset_height = (m_window_height - new_height) / 2.0f;
                View view = window.GetView();
                if (m_window_width >= aspectRatio * m_window_height)
                {
                    view.Viewport = new FloatRect(offset_width / m_window_width, 0.0f, new_width / m_window_width, 1.0f);
                }
                else
                {
                    view.Viewport = new FloatRect(0.0f, offset_height / m_window_height, 1.0f, new_height / m_window_height);
                }

                window.SetView(view);
            }

            protected override void DrawSpriteInternal(Sprite sprite)
            {
                var sfSprite = sfSprites[sprite.json];
                Vector2 pos = sprite.position - BBQLib.Camera + Size / 2;
                sfSprite.Position = new Vector2f(pos.X, pos.Y);
                sfSprite.Rotation = sprite.rotation;
                sfSprite.Origin = new Vector2f(sprite.origin.X, sprite.origin.Y);
                sfSprite.Scale = new Vector2f(sprite.scale.X, sprite.scale.Y);
                window.Draw(sfSprite);
            }

            public override void DrawLine(Vector2 A, Vector2 B, float width, Color color)
            {
                A = A - BBQLib.Camera + Size / 2;
                B = B - BBQLib.Camera + Size / 2;
                Vertex[] vertices = new Vertex[2]
                {
                    new Vertex(new Vector2f(A.X, A.Y), new SFML.Graphics.Color((byte)(color.r * 255), (byte)(color.g * 255), (byte)(color.b * 255), (byte)(color.a * 255))),
                    new Vertex(new Vector2f(B.X, B.Y), new SFML.Graphics.Color((byte)(color.r * 255), (byte)(color.g * 255), (byte)(color.b * 255), (byte)(color.a * 255)))
                };
                window.Draw(vertices, PrimitiveType.Lines);
            }
        }
    }
}