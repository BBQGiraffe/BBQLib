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

            Dictionary<string, SFML.Graphics.Sprite> sfSprites = new Dictionary<string, SFML.Graphics.Sprite>();
            Dictionary<string, SFML.Graphics.Text> sfTexts = new Dictionary<string, Text>();
            float deltaTime;
            readonly Clock deltaTimer = new Clock();
            static RenderWindow window;


            public SFMLWindow(WindowConfig config)
            {
                window = new RenderWindow(new VideoMode(config.width, config.height), config.name);
                window.SetFramerateLimit(config.fps);
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
                sprites.Clear();
                usedLayers.Clear();
                window.DispatchEvents();
                window.Clear();
                deltaTime = deltaTimer.Restart().AsSeconds();
            }

            public override void RegisterSprite(Sprite sprite, string name)
            {
                var sfSprite = new SFML.Graphics.Sprite(new Texture(sprite.name));
                sfSprites.Add(sprite.name, sfSprite);
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

            public override void Draw(Font font, string text, Vector2 position)
            {
                var sfText = sfTexts[font.jsonFilename];
                sfText.Position = new Vector2f(position.X, position.Y);
                sfText.DisplayedString = text;
                window.Draw(sfText);
            }

            public override void RegisterFont(Font font)
            {
                var sfFont = new SFML.Graphics.Font(font.ttf);
                var sfText = new Text("OWO yiff me harder daddy", sfFont);
                sfTexts.Add(font.jsonFilename, sfText);
            }

            protected override void DrawSpriteInternal(Sprite sprite)
            {
                var sfSprite = sfSprites[sprite.name];
                sfSprite.Position = new Vector2f(sprite.position.X, sprite.position.Y);
                sfSprite.Rotation = sprite.rotation;
                sfSprite.Origin = new Vector2f(sprite.origin.X, sprite.origin.Y);
                window.Draw(sfSprite);
            }
        }
    }
}