using System.Numerics;
namespace BBQLib
{
    public class BoundingBox
    {
        public Vector2 min = new Vector2();


        
        public Vector2 size = new Vector2();

        public Vector2 origin;

        public Vector2 max
        {
            get
            {
                return min + (size); 
            }
        }

        public Vector2 topRight
        {
            get
            {
                return new Vector2(min.X + size.X, min.Y);
            }
        }

        public Vector2 bottomCenter
        {
            get
            {
                return new Vector2(size.X / 2, size.Y);
            }
        }

        public Vector2 topCenter
        {
            get
            {
                return new Vector2(size.X / 2, 0);
            }
        }

        public static BoundingBox Create(Vector2 size)
        {
            return new BoundingBox(size);
        }

        public void SetOrigin(OriginType type)
        {
            switch (type)
            {
                case OriginType.topLeft:
                    origin = new Vector2();
                    break;
                case OriginType.topCenter:
                    origin = topCenter;
                    break;
                case OriginType.topRight:
                    origin = topRight;  
                    break;
                case OriginType.centerLeft:
                    break;
                case OriginType.centerCenter:
                    break;
                case OriginType.centerRight:
                    break;
                case OriginType.bottomLeft:
                    break;
                case OriginType.bottomCenter:
                    origin = bottomCenter;
                    break;
                case OriginType.bottomRight:
                    break;
            }
        }

        public static bool AABB(BoundingBox A, BoundingBox B)
        {
            return A.min.X <= B.max.X &&
                A.max.X >= B.min.X &&
                A.min.Y <= B.max.Y &&
                A.max.Y >= B.min.Y;
        }

        public BoundingBox(Vector2 size)
        {
            this.size = size;   
        }
    }
}