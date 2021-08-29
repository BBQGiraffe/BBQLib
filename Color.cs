using System;
namespace BBQLib
{
    public sealed class Color
    {
        public float r = 1, g = 1, b = 1, a = 1; 
        
        public Color()
        {

        }

        public Color(float r, float g, float b, float a = 1)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static Color operator + (Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b);
		}

        public static Color operator - (Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b);
		}

        public static Color operator * (Color a, float b)
        {
            return new Color(a.r * b, a.g * b, a.b * b);
        }

        public static Color Mix(Color A, Color B, float amount)
        {
            return (B - A) * amount + A;
        }


        public static Color CreateColor(float r, float g, float b, float a = 1)
        {
            return new Color(r,g,b,a);
        }

        public static readonly Color Red = new Color(1, 0, 0, .25f);
    }
}