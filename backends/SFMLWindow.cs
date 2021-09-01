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

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return base.ToString();
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
        }
    }
}