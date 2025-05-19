using System;
using UnityEngine;

namespace LightConnect.Model
{
    public class Tile
    {
        private bool _isActive;
        private Element _element = new();
        private WireSet _wireSet = new();

        public event Action Updated;
        public event Action EvaluationRequired;

        public Tile(Vector2Int position)
        {
            Position = position;
            ElementPowered = false;
        }

        public bool IsActive { get => _isActive; set => SetActive(value); }
        public Vector2Int Position { get; private set; }
        public bool ElementPowered { get; private set; }
        public WireSetTypes WireSetType => _wireSet.Type;
        public Direction Orientation => _wireSet.Orientation;
        public ElementTypes ElementType => _element.Type;
        public Color ElementColor => _element.Color;

        public TileData GetData()
        {
            var data = new TileData();

            data.PositionX = Position.x;
            data.PositionY = Position.y;
            data.WireType = (int)WireSetType;
            data.Orientation = (int)Orientation;
            data.ElementType = (int)ElementType;
            data.Color = (int)ElementColor;

            return data;
        }

        public void SetData(TileData data)
        {
            SetWireSetType((WireSetTypes)data.WireType);
            SetOrientation((Direction)data.Orientation);
            SetElementType((ElementTypes)data.ElementType);
            SetElementColor((Color)data.Color);

            Updated?.Invoke();
        }

        public void SetWireSetType(WireSetTypes type)
        {
            _wireSet.SetType(type);
            Updated?.Invoke();
            EvaluationRequired?.Invoke();
        }

        public void SetOrientation(Direction orientation)
        {
            _wireSet.SetOrientation(orientation);
            Updated?.Invoke();
            EvaluationRequired?.Invoke();
        }

        public void SetElementType(ElementTypes type)
        {
            _element.SetType(type);
            Updated?.Invoke();
            EvaluationRequired?.Invoke();
        }

        public void SetElementColor(Color color)
        {
            _element.SetColor(color);
            Updated?.Invoke();
            EvaluationRequired?.Invoke();
        }

        public void Rotate(Direction side)
        {
            _wireSet.Rotate(side);
            Updated?.Invoke();
            EvaluationRequired?.Invoke();
        }

        public void ApplyBatteryPower()
        {
            if (ElementType != ElementTypes.BATTERY)
                return;

            ElementPowered = true;
            _wireSet.AddColorToAllWires(ElementColor);

            Updated?.Invoke();
        }

        public bool HasWire(Direction direction)
        {
            return _wireSet.HasWire(direction);
        }

        public bool HasWire(Direction direction, out Color color)
        {
            return _wireSet.HasWire(direction, out color);
        }

        public void AddColor(Direction direction, Color color)
        {
            if (ElementType == ElementTypes.NONE)
            {
                _wireSet.AddColorToAllWires(color);
            }
            else
            {
                _wireSet.AddColor(direction, color);
            }

            if (ElementType == ElementTypes.LAMP)
                ElementPowered = ElementColor != Color.None && _wireSet.BlendedColor == ElementColor;

            Updated?.Invoke();
        }

        public void ResetColors()
        {
            _wireSet.ResetColors();
            ElementPowered = false;
            Updated?.Invoke();
        }

        private void SetActive(bool isActive)
        {
            _isActive = isActive;
            Updated?.Invoke();
            EvaluationRequired?.Invoke();
        }
    }
}