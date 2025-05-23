using UnityEngine;

namespace LightConnect.Model
{
    public class BatteryTile : Tile, IColoredTile, IRotatableTile
    {
        public BatteryTile(Vector2Int position) : base(position) { }

        public Color Color { get; private set; }

        public override TileTypes Type => TileTypes.BATTERY;
        public bool Powered => true;

        public void SetColor(Color color)
        {
            Color = color;
            InvokeUpdated();
            InvokeEvaluationRequired();
        }

        public override void ResetColors()
        {
            WireSet.ResetColors();
            AddColorToAllWires(Color);
            InvokeUpdated();
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