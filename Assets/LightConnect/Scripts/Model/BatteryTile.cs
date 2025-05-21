using UnityEngine;

namespace LightConnect.Model
{
    public class BatteryTile : Tile, IColoredTile
    {
        public BatteryTile(Vector2Int position) : base(position) { }

        public Color Color { get; private set; }

        public override TileTypes Type => TileTypes.BATTERY;
        public bool Powered => true;

        public void SetColor(Color color)
        {
            Color = color;
            InvokeUpdated();
        }

        public override void ResetColors()
        {
            WireSet.ResetColors();
            WireSet.AddColorToAllWires(Color);
            InvokeUpdated();
        }
    }
}