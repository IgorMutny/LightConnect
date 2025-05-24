using UnityEngine;

namespace LightConnect.Model
{
    public class LampTile : Tile, IColoredTile, IRotatableTile
    {
        public LampTile(Vector2Int position) : base(position) { }

        public override TileTypes Type => TileTypes.LAMP;
        public Color Color { get; private set; }
        public bool Powered { get; private set; }

        public void SetColor(Color color)
        {
            Color = color;
            InvokeContentChanged();
        }

        public override void AddColor(Direction direction, Color color)
        {
            WireSet.AddColor(direction, color);
            Powered = Color != Color.None && BlendedColor == Color;
            InvokeRecolorized();
        }

        public override void ResetColors()
        {
            WireSet.ResetColors();
            Powered = false;
            InvokeRecolorized();
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