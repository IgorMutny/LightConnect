using UnityEngine;

namespace LightConnect.Model
{
    public class LampTile : Tile, IColoredTile, IRotatableTile
    {
        public LampTile(Vector2Int position) : base(position) { }

        public override TileTypes Type => TileTypes.LAMP;
        public Color Color { get; private set; }
        public bool ElementPowered { get; private set; }

        public void SetElementColor(Color color)
        {
            Color = color;
            InvokeContentChanged();
        }

        public override void AddColor(Direction direction, Color color, int order)
        {
            WireSet.AddColor(direction, color);
            PoweringOrder = Mathf.Min(order, PoweringOrder);
            ElementPowered = Color != Color.None && BlendedColor == Color;
        }

        public override void ResetColors()
        {
            WireSet.ResetColors();
            ElementPowered = false;
            PoweringOrder = int.MaxValue;
        }

        protected override void WriteAdditionalData(TileData data)
        {
            data.Color = Color; 
        }

        protected override void ApplyAdditionalData(TileData data)
        {
            Color = data.Color;
        }
    }
}