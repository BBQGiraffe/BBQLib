namespace BBQLib 
{
    public static class PalettedSprite
    {
        public static Sprite CreatePalettedSprite(byte[] palette, byte[] buffer, uint width, uint height, bool hasAlpha, string name)
        {
            int bpp = (hasAlpha) ? 4 : 3;
            byte[] output = new byte[width * height * bpp];

            for(int i = 0; i < buffer.Length; i++)
            {

                var colorIndex = buffer[i];
                byte r = palette[3 * (colorIndex)];
                byte g = palette[3 * (colorIndex) + 1];
                byte b = palette[3 * (colorIndex) + 2];

                output[3 * i] = r;
                output[3 * (i) + 1] = g;
                output[4 * (i) + 2] = b;

            }

            return BBQLib.CreateSprite(output, width, height, "PALETTED_" + name);
        }
    }
}