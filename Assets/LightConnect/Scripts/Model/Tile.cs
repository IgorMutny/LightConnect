using System;
using UnityEngine;

namespace LightConnect.Model
{
    public abstract class Tile
    {
        protected WireSet WireSet = new();

        public event Action ContentChanged;
        public event Action EvaluationRequired;
        public event Action Recolorized;

        public Tile(Vector2Int position)
        {
            Position = position;
        }

        public abstract TileTypes Type { get; }
        public Vector2Int Position { get; private set; }
        public WireSetTypes WireSetType => WireSet.Type;
        public Direction Orientation => WireSet.Orientation;
        public Color BlendedColor => WireSet.BlendedColor;
        public int OrderInPowerChain { get; set; }

        public TileData GetData()
        {
            var data = new TileData();
            data.Type = Type;
            data.Position = Position;
            data.WireSetType = WireSetType;
            data.Orientation = Orientation;
            WriteAdditionalData(data);
            return data;
        }

        public void SetData(TileData data)
        {
            WireSet.SetType(data.WireSetType);
            WireSet.SetOrientation(data.Orientation);
            ApplyAdditionalData(data);
            InvokeContentChanged();
        }

        public void SetWireSetType(WireSetTypes type)
        {
            WireSet.SetType(type);
            InvokeContentChanged();
        }

        public void SetOrientation(Direction orientation)
        {
            WireSet.SetOrientation(orientation);
            InvokeContentChanged();
        }

        public void Rotate(Direction side)
        {
            WireSet.Rotate(side);
            InvokeContentChanged();
        }

        public bool HasWire(Direction direction)
        {
            return WireSet.HasWire(direction);
        }

        public bool HasWire(Direction direction, out Color color)
        {
            return WireSet.HasWire(direction, out color);
        }

        public bool HasAnyColorInWires()
        {
            return WireSet.HasAnyColorInWires();
        }

        public virtual void AddColor(Direction direction, Color color)
        {
            WireSet.AddColor(direction, color);
            InvokeRecolorized();
        }

        public void AddColorToAllWires(Color color)
        {
            WireSet.AddColorToAllWires(color);
            InvokeRecolorized();
        }

        public virtual void ResetColors()
        {
            WireSet.ResetColors();
            InvokeRecolorized();
        }

        protected void InvokeContentChanged()
        {
            ContentChanged?.Invoke();
            EvaluationRequired?.Invoke();
        }

        protected void InvokeRecolorized()
        {
            Recolorized?.Invoke();
        }

        protected virtual void WriteAdditionalData(TileData data) { }
        protected virtual void ApplyAdditionalData(TileData data) { }
    }
}