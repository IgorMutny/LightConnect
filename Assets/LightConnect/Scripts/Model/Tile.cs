using R3;
using UnityEngine;

namespace LightConnect.Model
{
    public class Tile
    {
        public readonly Vector2Int Position;
        public ReactiveProperty<bool> IsActive = new();
        public ReactiveProperty<bool> IsSelected = new();

        private ReactiveProperty<bool> _powered = new();
        private Wire _wire = new();
        private Element _element = new();

        public Tile(Vector2Int position)
        {
            Position = position;
            _powered.Value = false;
        }

        public ReadOnlyReactiveProperty<Direction> Orientation => _wire.Orientation;
        public ReadOnlyReactiveProperty<bool> Powered => _powered;
        public ReadOnlyReactiveProperty<WireTypes> WireType => _wire.Type;
        public ReadOnlyReactiveProperty<ElementTypes> ElementType => _element.Type;
        public ReadOnlyReactiveProperty<Colors> WireColor => _wire.Color;
        public ReadOnlyReactiveProperty<Colors> ElementColor => _element.Color;

        public TileData GetData()
        {
            var data = new TileData();

            data.PositionX = Position.x;
            data.PositionY = Position.y;
            data.WireType = (int)WireType.CurrentValue;
            data.Orientation = (int)Orientation.CurrentValue.Side;
            data.ElementType = (int)ElementType.CurrentValue;
            data.Color = (int)ElementColor.CurrentValue;

            return data;
        }

        public void SetData(TileData data)
        {
            SetWireType((WireTypes)data.WireType);
            SetOrientation((Sides)data.Orientation);
            SetElementType((ElementTypes)data.ElementType);
            SetColor((Colors)data.Color);
        }

        public void SetWireType(WireTypes type)
        {
            _wire.SetType(type);
            OnContentChanged();
        }

        public void SetOrientation(Sides side)
        {
            _wire.SetOrientation(side);
            OnContentChanged();
        }

        public void SetElementType(ElementTypes type)
        {
            _element.SetType(type);
            OnContentChanged();
        }

        public void SetColor(Colors color)
        {
            _element.SetColor(color);
            OnContentChanged();
        }

        public void Rotate(Sides side)
        {
            _wire.Rotate(side);
        }

        public bool HasConnectorInDirection(Sides direction)
        {
            return _wire.HasConnectorInDirection(direction);
        }

        public void AddColor(Colors color)
        {
            if (WireType.CurrentValue == WireTypes.NONE || ElementType.CurrentValue == ElementTypes.BATTERY)
                return;

            _wire.AddColor(color);

            if (ElementType.CurrentValue == ElementTypes.NONE)
                _powered.Value = WireColor.CurrentValue != Colors.NONE;
            else
                _powered.Value = WireColor.CurrentValue != Colors.NONE && WireColor.CurrentValue == ElementColor.CurrentValue;
        }

        public void ResetPower()
        {
            if (ElementType.CurrentValue == ElementTypes.BATTERY)
                return;

            _wire.ResetColors();
            _powered.Value = false;
        }

        private void OnContentChanged()
        {
            if (ElementType.CurrentValue == ElementTypes.BATTERY)
            {
                _wire.AddColor(ElementColor.CurrentValue);
                _powered.Value = true;
            }
        }
    }
}