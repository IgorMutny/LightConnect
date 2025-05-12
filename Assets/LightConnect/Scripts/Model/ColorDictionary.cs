using System;
using UnityEngine;

namespace LightConnect.Model
{
    public static class ColorDictionary
    {
        public static Color ColorOfWire(Colors color)
        {
            switch (color)
            {
                case Colors.NONE: return Color.white;
                case Colors.RED: return Color.red;
                case Colors.GREEN: return Color.green;
                case Colors.BLUE: return Color.blue;
                default: throw new Exception("Unknown color type");
            }
        }

        public static Color ColorOfElement(Colors color, bool powered)
        {
            if (powered)
                return ColorOfPoweredElement(color);
            else
                return ColorOfNotPoweredElement(color);
        }

        private static Color ColorOfPoweredElement(Colors color)
        {
            switch (color)
            {
                case Colors.NONE: return Color.white;
                case Colors.RED: return Color.red;
                case Colors.GREEN: return Color.green;
                case Colors.BLUE: return Color.blue;
                default: throw new Exception("Unknown color type");
            }
        }

        private static Color ColorOfNotPoweredElement(Colors color)
        {
            switch (color)
            {
                case Colors.NONE: return Color.grey;
                case Colors.RED: return new Color(0.5f, 0f, 0f);
                case Colors.GREEN: return new Color(0f, 0.5f, 0f);
                case Colors.BLUE: return new Color(0f, 0f, 0.5f);
                default: throw new Exception("Unknown color type");
            }
        }
    }
}