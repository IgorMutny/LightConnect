using UnityEngine;

namespace LightConnect.Model
{
    public class BatteryTile : Tile
    {
        public BatteryTile(Vector2Int position) : base(position) { }

        public Color Color { get; private set; }

        public override TileTypes Type => TileTypes.BATTERY;

        public void SetColor(Color color)
        {
            Color = color;
        }

        public override void ResetColors()
        {
            WireSet.ResetColors();
            WireSet.AddColorToAllWires(Color);
            InvokeUpdated();
        }
    }
}