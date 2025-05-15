using System;

namespace LightConnect.Model
{
    public struct Direction : IEquatable<Direction>
    {
        public const int DIRECTIONS_COUNT = 4;

        private const int UP = 0;
        private const int RIGHT = 1;
        private const int DOWN = 2;
        private const int LEFT = 3;

        private int _value;

        private Direction(int value)
        {
            _value = value;
        }

        public static Direction Up => new Direction(UP);
        public static Direction Down => new Direction(DOWN);
        public static Direction Left => new Direction(LEFT);
        public static Direction Right => new Direction(RIGHT);

        public static Direction operator +(Direction a, Direction b)
        {
            return new Direction((a._value + b._value) % DIRECTIONS_COUNT);
        }

        public static Direction operator -(Direction a, Direction b)
        {
            return new Direction((DIRECTIONS_COUNT + a._value - b._value) % DIRECTIONS_COUNT);
        }

        public static Direction operator -(Direction a)
        {
            return new Direction((a._value + DIRECTIONS_COUNT / 2) % DIRECTIONS_COUNT);
        }

        public static bool operator ==(Direction a, Direction b) => a._value == b._value;
        public static bool operator !=(Direction a, Direction b) => a._value != b._value;

        public bool Equals(Direction other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is Direction direction && Equals(direction);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            if (this == Up) return nameof(Up);
            if (this == Down) return nameof(Down);
            if (this == Left) return nameof(Left);
            if (this == Right) return nameof(Right);
            throw new Exception("Unknown direction");
        }

        public static explicit operator int(Direction direction)
        {
            return direction._value;
        }

        public static explicit operator Direction(int value)
        {
            return new Direction(value);
        }
    }
}