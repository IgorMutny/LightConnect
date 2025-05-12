using System;

namespace LightConnect.Model
{
    public struct Direction : IEquatable<Direction>
    {
        private const int SIDES_COUNT = 4;

        public Direction(Sides initialDirection)
        {
            Side = initialDirection;
        }

        public Sides Side { get; private set; }

        public static Direction operator +(Direction a, Direction b)
        {
            return a + b.Side;
        }

        public static Direction operator +(Direction a, Sides b)
        {
            int newSide = ((int)a.Side + (int)b) % SIDES_COUNT;
            return new Direction((Sides)newSide);
        }

        public static bool operator ==(Direction a, Sides b) => a.Side == b;
        public static bool operator !=(Direction a, Sides b) => a.Side != b;
        public static bool operator ==(Direction a, Direction b) => a.Side == b.Side;
        public static bool operator !=(Direction a, Direction b) => a.Side != b.Side;

        public bool Equals(Direction other)
        {
            return Side == other.Side;
        }

        public bool Equals(Sides side)
        {
            return Side == side;
        }

        public override bool Equals(object obj)
        {
            return obj is Direction dir && Equals(dir);
        }

        public override int GetHashCode()
        {
            return Side.GetHashCode();
        }
    }
}