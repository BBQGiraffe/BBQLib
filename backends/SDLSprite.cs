using System;
using static SDL2.SDL;
namespace BBQLib
{
    namespace Backends
    {
        internal class SDLSprite
        {
            public IntPtr texture;
            public SDL_Rect rect = new SDL_Rect();
        }
    }
}