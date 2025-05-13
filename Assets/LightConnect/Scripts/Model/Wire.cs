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
        private ColorList _addedColors = new();

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
            _orientation.Value = new Direction(Sides.UP);
            _connectors = ConnectorDictionary.ConnectorsOfWire(type);

            for (int i = 0; i < _connectors.Count; i++)
                _connectors[i] += _orientation.Value;

            _color.Value = Colors.NONE;
            _type.Value = type;
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
            _addedColors.Add(color);
            _color.Value = _addedColors.GetCurrentColor();
        }

        public void ResetColors()
        {
            _addedColors.Clear();
            _color.Value = _addedColors.GetCurrentColor();
        }

        public bool HasConnectorInDirection(Sides direction)
        {
            return _connectors.Any(connector => connector == direction);
        }
    }
}