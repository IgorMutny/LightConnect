using UnityEngine;

namespace LightConnect.Model
{
    public class LampTile : Tile, IColoredTile
    {
        public LampTile(Vector2Int position) : base(position) { }

        public override TileTypes Type => TileTypes.LAMP;
        public Color Color { get; private set; }
        public bool Powered { get; private set; }

        public void SetColor(Color color)
        {
            Color = color;
            InvokeUpdated();
        }

        public override void AddColor(Direction direction, Color color)
        {
            WireSet.AddColor(direction, color);
            Powered = Color != Color.None && WireSet.BlendedColor == Color;
            InvokeUpdated();
        }

        public override void ResetColors()
        {
            WireSet.ResetColors();
            Powered = false;
            InvokeUpdated();
        }
    }
}