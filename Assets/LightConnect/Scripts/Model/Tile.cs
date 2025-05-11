using R3;
using UnityEngine;

namespace LightConnect.Model
{
    public class Tile
    {
        public readonly Vector2Int Position;

        private ReactiveProperty<Sides> _orientation = new();
        private ReactiveProperty<bool> _powered = new();
        private Wire _wire;
        private Element _element;
        private ElementTypes _elementType;

        public Tile(Vector2Int position)
        {
            Position = position;
            _orientation.Value = Sides.UP;
            _powered.Value = false;
        }

        public ReadOnlyReactiveProperty<Sides> Orientation => _orientation;
        public ReadOnlyReactiveProperty<bool> Powered => _powered;
        public Colors WireColor => _wire != null ? _wire.CurrentColor : Colors.NONE;
        public Colors ElementColor => _element != null ? _element.Color : Colors.NONE;
        public ElementTypes ElementType => _elementType;

        public void SetWire(Wire wire)
        {
            _wire = wire;

            if (_wire != null)
                _orientation.Value = _wire.Orientation;
            else
                _orientation.Value = Sides.UP;
        }

        public void SetElement(Element element)
        {
            _element = element;

            if (_element is Battery)
            {
                _wire.AddPower(_element.Color);
                _powered.Value = true;
            }
            else
            {
                _powered.Value = false;
            }
        }

        public void RotateRight()
        {
            _wire.RotateRight();
            _orientation.Value = _wire.Orientation;
        }

        public void RotateLeft()
        {
            _wire.RotateLeft();
            _orientation.Value = _wire.Orientation;
        }

        public bool HasConnectorInDirection(Sides direction)
        {
            return _wire.HasConnectorInDirection(direction);
        }

        public void AddPower(Colors color)
        {
            if (_element is Battery)
                return;

            _wire.AddPower(color);

            if (_element == null)
                _powered.Value = _wire.CurrentColor != Colors.NONE;
            else
                _powered.Value = _wire.CurrentColor != Colors.NONE && _wire.CurrentColor == _element.Color;
        }

        public void ResetPower()
        {
            if (_element is Battery)
                return;

            _wire.ResetPower();
            _powered.Value = false;
        }
    }
}