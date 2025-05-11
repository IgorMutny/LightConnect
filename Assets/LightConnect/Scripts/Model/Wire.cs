using System.Collections.Generic;
using System.Linq;

namespace LightConnect.Model
{
    public class Wire
    {
        public readonly WireTypes Type;

        private List<Direction> _connectors;
        private List<Colors> _appliedColors;
        private Direction _orientation;

        public Wire(WireTypes type, Direction orientation)
        {
            Type = type;
            _orientation = orientation;
            _connectors = ConnectorHelper.ConnectorsOfWire(type);
            _appliedColors = new();
            CurrentColor = Colors.NONE;
        }

        public Sides Orientation => _orientation.Side;
        public Colors CurrentColor { get; private set; }

        public void RotateRight()
        {
            foreach (var connector in _connectors)
                connector.RotateRight();

            _orientation.RotateRight();
        }

        public void RotateLeft()
        {
            foreach (var connector in _connectors)
                connector.RotateLeft();

            _orientation.RotateLeft();
        }

        public void AddPower(Colors color)
        {
            _appliedColors.Add(color);

            if (AllPowersAreOfSameColor())
                CurrentColor = _appliedColors[0];
            else
                CurrentColor = Colors.NONE;
        }

        public void ResetPower()
        {
            _appliedColors.Clear();
            CurrentColor = Colors.NONE;
        }

        public bool HasConnectorInDirection(Sides direction)
        {
            return _connectors.Any(connector => Direction.Add(connector.Side, _orientation.Side) == direction);
        }

        private bool AllPowersAreOfSameColor()
        {
            if (_appliedColors.Count == 0)
                return false;

            Colors firstColor = _appliedColors[0];

            foreach (var color in _appliedColors)
                if (color != firstColor)
                    return false;

            return true;
        }
    }
}