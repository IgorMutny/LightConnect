using System;
using System.Collections.Generic;

namespace LightConnect.Core
{
    public class Tile
    {
        private const int ORIENTATION_COUNT = 4;
        private int _orientation;
        private List<int> _connectors;

        public event Action Rotated;

        public Tile(int orientation, List<int> connectors)
        {
            _orientation = orientation;
            _connectors = connectors;
        }

        public int Orientation => _orientation;

        public void Rotate()
        {
            _orientation += 1;

            if (_orientation == ORIENTATION_COUNT)
                _orientation = 0;

            Rotated?.Invoke();
        }
    }
}