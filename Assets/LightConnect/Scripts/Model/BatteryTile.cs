using UnityEngine;

namespace LightConnect.Model
{
    public class BatteryTile : Tile, IColoredTile, IRotatableTile
    {
        public BatteryTile(Vector2Int position) : base(position) { }

        public Color Color { get; private set; }

        public override TileTypes Type => TileTypes.BATTERY;
        public bool ElementPowered => true;

        public void SetElementColor(Color color)
        {
            Color = color;
            InvokeEvaluation();
        }

        public override void ResetColors()
        {
            WireSet.ResetColors();
            PoweringOrder = 0;
            AddColorToAllWires(Color, PoweringOrder);
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