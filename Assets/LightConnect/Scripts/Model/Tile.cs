using System;
using UnityEngine;

namespace LightConnect.Model
{
    public class Tile
    {
        private bool _isActive;
        private bool _isSelected;
        private Element _element = new();
        private WireSet _wireSet = new();

        public event Action EvaluationRequired;
        public event Action Updated;
        public event Action<Tile> Selected;

        public Tile(Vector2Int position)
        {
            Position = position;
            ElementPowered = false;
        }

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; EvaluationRequired?.Invoke(); }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; Selected?.Invoke(this); }
        }

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

            RequireUpdate();
        }

        public void SetWireSetType(WireSetTypes type)
        {
            _wireSet.SetType(type);
            RequireUpdate();
            RequireEvaluation();
        }

        public void SetOrientation(Direction orientation)
        {
            _wireSet.SetOrientation(orientation);
            RequireUpdate();
            RequireEvaluation();
        }

        public void SetElementType(ElementTypes type)
        {
            _element.SetType(type);
            RequireUpdate();
            RequireEvaluation();
        }

        public void SetElementColor(Color color)
        {
            _element.SetColor(color);
            RequireUpdate();
            RequireEvaluation();
        }

        public void Rotate(Direction side)
        {
            _wireSet.Rotate(side);
            RequireUpdate();
            RequireEvaluation();
        }

        public void ApplyBatteryPower()
        {
            if (ElementType != ElementTypes.BATTERY)
                return;

            ElementPowered = true;
            _wireSet.AddColorToAllWires(ElementColor);

            RequireUpdate();
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

            RequireUpdate();
        }

        public void ResetColors()
        {
            _wireSet.ResetColors();
            ElementPowered = false;

            RequireUpdate();
        }

        private void RequireUpdate()
        {
            Updated?.Invoke();
        }

        private void RequireEvaluation()
        {
            EvaluationRequired?.Invoke();
        }
    }
}