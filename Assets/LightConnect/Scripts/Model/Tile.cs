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

        public TileData GetData()
        {
            var data = new TileData();

            data.PositionX = Position.x;
            data.PositionY = Position.y;
            data.WireType = (int)WireSetType;
            data.Orientation = (int)Orientation;
            /* data.ElementType = (int)ElementType;
            data.Color = (int)ElementColor; */

            return data;
        }

        public void SetData(TileData data)
        {
            SetWireSetType((WireSetTypes)data.WireType);
            SetOrientation((Direction)data.Orientation);
            /* SetElementType((ElementTypes)data.ElementType);
            SetElementColor((Color)data.Color); */

            Updated?.Invoke();
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
    }
}