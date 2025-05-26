using System;
using UnityEngine;

namespace LightConnect.Model
{
    public abstract class Tile
    {
        protected WireSet WireSet = new();

        public event Action EvaluationRequired;
        public event Action RedrawingRequired;

        public Tile(Vector2Int position)
        {
            Position = position;
        }

        public abstract TileTypes Type { get; }
        public Vector2Int Position { get; private set; }
        public WireSetTypes WireSetType => WireSet.Type;
        public Direction Orientation => WireSet.Orientation;
        public Color BlendedColor => WireSet.BlendedColor;
        public int PoweringOrder { get; protected set; }
        public bool WiresPowered => WireSet.HasAnyColoredWires();
        public bool Locked { get; protected set; }

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
            InvokeEvaluation();
        }

        public void SetWireSetType(WireSetTypes type)
        {
            WireSet.SetType(type);
            InvokeEvaluation();
        }

        public void SetOrientation(Direction orientation)
        {
            WireSet.SetOrientation(orientation);
            InvokeEvaluation();
        }

        public void Rotate(Direction side)
        {
            WireSet.Rotate(side);
            InvokeEvaluation();
        }

        public bool HasWire(Direction direction)
        {
            return WireSet.HasWire(direction);
        }

        public bool HasWire(Direction direction, out Color color)
        {
            return WireSet.HasWire(direction, out color);
        }

        public virtual void AddColor(Direction direction, Color color, int order)
        {
            WireSet.AddColor(direction, color);
            PoweringOrder = Mathf.Min(order, PoweringOrder);
        }

        public void AddColorToAllWires(Color color, int order)
        {
            WireSet.AddColorToAllWires(color);
            PoweringOrder = Mathf.Min(order, PoweringOrder);
        }

        public virtual void ResetColors()
        {
            WireSet.ResetColors();
            PoweringOrder = int.MaxValue;
        }

        public void InvokeRedrawing()
        {
            RedrawingRequired?.Invoke();
        }

        protected void InvokeEvaluation()
        {
            EvaluationRequired?.Invoke();
        }

        protected virtual void WriteAdditionalData(TileData data) { }
        protected virtual void ApplyAdditionalData(TileData data) { }
    }
}