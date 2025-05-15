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

        public event Action<Vector2Int> Updated;
        public event Action UpdatedInternal;
        public event Action<Tile> Selected;

        public Tile(Vector2Int position)
        {
            Position = position;
            ElementPowered = false;
        }

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; Updated?.Invoke(Position); }
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

            OnStateChanged();
        }

        public void SetWireSetType(WireSetTypes type)
        {
            _wireSet.SetType(type);
            OnStateChanged();
        }

        public void SetOrientation(Direction orientation)
        {
            _wireSet.SetOrientation(orientation);
            OnStateChanged();
        }

        public void SetElementType(ElementTypes type)
        {
            _element.SetType(type);
            OnStateChanged();
        }

        public void SetElementColor(Color color)
        {
            _element.SetColor(color);
            OnStateChanged();
        }

        public void Rotate(Direction side)
        {
            _wireSet.Rotate(side);
            OnStateChanged();
        }

        public bool HasWire(Direction direction)
        {
            return _wireSet.HasWire(direction);
        }

        public bool HasWire(Direction direction, out Color color)
        {
            return _wireSet.HasWire(direction, out color);
        }

        [Obsolete]
        public void AddColor(Direction direction, Color color)
        {
            _wireSet.AddColor(direction, color, ElementType != ElementTypes.NONE);
            ElementPowered = ElementColor != Color.None && _wireSet.HasColor(ElementColor);
            OnStateChangedNoUpdate();
        }

        [Obsolete]
        public void ResetColors()
        {
            _wireSet.ResetColors();
            ElementPowered = false;
            OnStateChangedNoUpdate();
        }

        [Obsolete]
        private void OnStateChanged()
        {
            if (ElementType == ElementTypes.BATTERY)
            {
                ElementPowered = true;
                _wireSet.AddColorToAll(ElementColor);
            }

            UpdatedInternal?.Invoke();
            Updated?.Invoke(Position);
        }

        [Obsolete]
        private void OnStateChangedNoUpdate()
        {
            if (ElementType == ElementTypes.BATTERY)
            {
                ElementPowered = true;
                _wireSet.AddColorToAll(ElementColor);
            }

            UpdatedInternal?.Invoke();
        }
    }
}