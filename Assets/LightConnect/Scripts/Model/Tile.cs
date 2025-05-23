using System;
using UnityEngine;

namespace LightConnect.Model
{
    public abstract class Tile
    {
        protected WireSet WireSet = new();

        public event Action Updated;
        public event Action EvaluationRequired;

        public Tile(Vector2Int position)
        {
            Position = position;
        }

        public abstract TileTypes Type { get; }
        public Vector2Int Position { get; private set; }
        public WireSetTypes WireSetType => WireSet.Type;
        public Direction Orientation => WireSet.Orientation;
        public Color BlendedColor => WireSet.BlendedColor;

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
            InvokeUpdated();
            InvokeEvaluationRequired();
        }

        public void SetWireSetType(WireSetTypes type)
        {
            WireSet.SetType(type);
            InvokeUpdated();
            InvokeEvaluationRequired();
        }

        public void SetOrientation(Direction orientation)
        {
            WireSet.SetOrientation(orientation);
            InvokeUpdated();
            InvokeEvaluationRequired();
        }

        public void Rotate(Direction side)
        {
            WireSet.Rotate(side);
            InvokeUpdated();
            InvokeEvaluationRequired();
        }

        public bool HasWire(Direction direction)
        {
            return WireSet.HasWire(direction);
        }

        public bool HasWire(Direction direction, out Color color)
        {
            return WireSet.HasWire(direction, out color);
        }

        public virtual void AddColor(Direction direction, Color color)
        {
            WireSet.AddColor(direction, color);
            InvokeUpdated();
        }

        public void AddColorToAllWires(Color color)
        {
            WireSet.AddColorToAllWires(color);
            InvokeUpdated();
        }

        public virtual void ResetColors()
        {
            WireSet.ResetColors();
            InvokeUpdated();
        }

        protected void InvokeUpdated()
        {
            Updated?.Invoke();
        }

        protected void InvokeEvaluationRequired()
        {
            EvaluationRequired?.Invoke();
        }

        protected virtual void WriteAdditionalData(TileData data) { }
        protected virtual void ApplyAdditionalData(TileData data) { }
    }
}