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
            List<Sprite> sprites = new List<Sprite>();


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

            float deltaTime;
            Clock deltaTimer = new Clock();


            private static RenderWindow window;

            readonly List<int> usedLayers = new List<int>();
            
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
                sfSprites.Add(name, sfSprite);
            }

            public override void Draw(Sprite sprite)
            {
                sprite.drawn = false;
                if(!usedLayers.Contains(sprite.layer))
                {
                    usedLayers.Add(sprite.layer);
                }
                sprites.Add(sprite);
            }

            public override void Present()
            {
                usedLayers.Sort();
                foreach(var layer in usedLayers)
                {
                    for(int i = 0; i < sprites.Count; i++)
                    {
                        Sprite sprite = sprites[i];
                        if(!sprite.drawn)
                        {
                            if(sprite.layer == layer)
                            {
                                var sfSprite = sfSprites[sprite.json];
                                sprite.drawn = true;

                                sfSprite.Position = new Vector2f(sprite.position.X, sprite.position.Y);
                                sfSprite.Rotation = sprite.rotation;
                                sfSprite.Texture.Repeated = sprite.repeat;
                                sfSprite.Origin = new Vector2f(sprite.origin.X, sprite.origin.Y);
                                window.Draw(sfSprite);
                            }
                        }
                        
                    }
                }
                
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
        }
    }
}