using System;

namespace LightConnect.Model
{
    public struct Color : IEquatable<Color>
    {
        private const int NONE = 0;
        private const int RED = 1;
        private const int YELLOW = 2;
        private const int ORANGE = 3;
        private const int BLUE = 4;
        private const int MAGENTA = 5;
        private const int GREEN = 6;
        private const int WHITE = 7;

        private int _value;

        private Color(int value)
        {
            _value = value;
        }

        public static Color None => new Color(NONE);
        public static Color Red => new Color(RED);
        public static Color Yellow => new Color(YELLOW);
        public static Color Orange => new Color(ORANGE);
        public static Color Blue => new Color(BLUE);
        public static Color Magenta => new Color(MAGENTA);
        public static Color Green => new Color(GREEN);
        public static Color White => new Color(WHITE);

        public static Color operator +(Color a, Color b)
        {
            return new Color(a._value | b._value);
        }

        public static bool operator ==(Color a, Color b) => a._value == b._value;
        public static bool operator !=(Color a, Color b) => a._value != b._value;

        public bool Equals(Color other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is Color color && Equals(color);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static explicit operator int(Color color)
        {
            return color._value;
        }

        public static explicit operator Color(int value)
        {
            return new Color(value);
        }
    }
}