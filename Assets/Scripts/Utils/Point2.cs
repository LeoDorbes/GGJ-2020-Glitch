using System;

namespace Utils
{
    [Serializable]
    public class Point2
    {
        public int X;
        public int Y;

        #region constructors
        public Point2(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public Point2(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }

        public Point2(Point2 point2)
        {
            X = point2.X;
            Y = point2.Y;
        }
        #endregion

        public static Point2 Zero
        {
            get { return new Point2(); }
        }

        #region operator overrides
        public static bool operator ==(Point2 first, Point2 second)
        {
            return ReferenceEquals(first, null) ? ReferenceEquals(second, null) : first.Equals(second);
        }

        public static bool operator !=(Point2 first, Point2 second)
        {
            return !(first == second);
        }

        public static Point2 operator +(Point2 first, Point2 second)
        {
            return new Point2(first.X + second.X, first.Y + second.Y);
        }

        public static Point2 operator -(Point2 first, Point2 second)
        {
            return new Point2(first.X - second.X, first.Y - second.Y);
        }

        public static Point2 operator *(Point2 first, int second)
        {
            return new Point2(first.X * second, first.Y * second);
        }
        #endregion

        public override int GetHashCode()
        {
            return (X << 2) ^ Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
            {
                return false;
            }
            Point2 p = (Point2)obj;
            return (X == p.X) && (Y == p.Y);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}