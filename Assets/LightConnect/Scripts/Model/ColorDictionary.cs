using System.Collections.Generic;
using UnityColor = UnityEngine.Color;

namespace LightConnect.Model
{
    public static class ColorDictionary
    {
        private static Dictionary<Color, UnityColor> _poweredColors = new()
        {
            {Color.None, UnityColor.gray},
            {Color.Red, UnityColor.red},
            {Color.Yellow, UnityColor.yellow},
            {Color.Orange, new UnityColor(1f, 0.5f, 0f)},
            {Color.Blue, UnityColor.blue},
            {Color.Magenta, UnityColor.magenta},
            {Color.Green, UnityColor.green},
            {Color.White, UnityColor.white},
        };

        private static Dictionary<Color, UnityColor> _notPoweredColors = new()
        {
            {Color.None, UnityColor.gray},
            {Color.Red, new UnityColor(0.5f, 0f, 0f)},
            {Color.Yellow, new UnityColor(0.5f, 0.5f, 0f)},
            {Color.Orange, new UnityColor(0.5f, 0.25f, 0f)},
            {Color.Blue, new UnityColor(0f, 0f, 0.5f)},
            {Color.Magenta, new UnityColor(0.5f, 0f, 0.5f)},
            {Color.Green, new UnityColor(0f, 0.5f, 0f)},
            {Color.White, UnityColor.gray},
        };

        public static UnityColor GetColor(Color color, bool powered)
        {
            if (powered)
                return _poweredColors[color];
            else
                return _notPoweredColors[color];
        }
    }
}