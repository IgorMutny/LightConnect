using System;

namespace LightConnect.Core
{
    public class Tile
    {
        private const int ORIENTATION_COUNT = 4;
        private int _orientation;

        public event Action<int> Rotated;

        public void Rotate()
        {
            _orientation += 1;

            if (_orientation == ORIENTATION_COUNT)
                _orientation = 0;

            Rotated?.Invoke(_orientation);
        }
    }
}