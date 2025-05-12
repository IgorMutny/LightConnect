using System.Collections.Generic;
using System.Linq;
using R3;

namespace LightConnect.Model
{
    public class Wire
    {
        private ReactiveProperty<WireTypes> _type = new();
        private ReactiveProperty<Direction> _orientation = new();
        private ReactiveProperty<Colors> _color = new();
        private List<Direction> _connectors = new();
        private List<Colors> _appliedColors = new();

        public Wire()
        {
            _type.Value = WireTypes.NONE;
            _orientation.Value = new Direction(Sides.UP);
            _color.Value = Colors.NONE;
        }

        public ReadOnlyReactiveProperty<WireTypes> Type => _type;
        public ReadOnlyReactiveProperty<Direction> Orientation => _orientation;
        public ReadOnlyReactiveProperty<Colors> Color => _color;

        public void SetType(WireTypes type)
        {
            _type.Value = type;
            _connectors = ConnectorHelper.ConnectorsOfWire(type);

            for (int i = 0; i < _connectors.Count; i++)
                _connectors[i] += _orientation.Value;

            _color.Value = Colors.NONE;
        }

        public void SetOrientation(Sides side)
        {
            _orientation.Value = new Direction(side);

            for (int i = 0; i < _connectors.Count; i++)
                _connectors[i] += _orientation.Value;

            _color.Value = Colors.NONE;
        }

        public void Rotate(Sides side)
        {
            for (int i = 0; i < _connectors.Count; i++)
                _connectors[i] += side;

            _orientation.Value += side;
        }

        public void AddColor(Colors color)
        {
            _appliedColors.Add(color);

            if (_appliedColors.All(appliedColor => appliedColor == color))
                _color.Value = _appliedColors[0];
            else
                _color.Value = Colors.NONE;
        }

        public void ResetColors()
        {
            _appliedColors.Clear();
            _color.Value = Colors.NONE;
        }

        public bool HasConnectorInDirection(Sides direction)
        {
            return _connectors.Any(connector => connector == direction);
        }
    }
}