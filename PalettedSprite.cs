using System;
namespace BBQLib 
{
    public static class PalettedSprite
    {
        public static Sprite CreatePalettedSprite(byte[] palette, byte[] buffer, uint width, uint height, bool hasAlpha, string name)
        {
            int bpp = (hasAlpha) ? 4 : 3;
            byte[] output = new byte[width * height * 4];

            for(int i = 0; i < buffer.Length; i++)
            {

                var colorIndex = buffer[i];
                byte r = palette[bpp * (colorIndex)];
                byte g = palette[bpp * (colorIndex) + 1];
                byte b = palette[bpp * (colorIndex) + 2];
                byte a = (hasAlpha) ? palette[bpp * (colorIndex) + 3] : byte.MaxValue;

                output[4 * i] = r;
                output[4 * (i) + 1] = g;
                output[4 * (i) + 2] = b;
                output[4 * (i) + 3] = a;

            }

            return BBQLib.CreateSprite(output, width, height, "PALETTED_" + name);
        }
    }
}