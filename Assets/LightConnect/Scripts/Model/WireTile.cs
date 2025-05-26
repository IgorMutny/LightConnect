using UnityEngine;

namespace LightConnect.Model
{
    public class WireTile : Tile, IRotatableTile
    {
        public WireTile(Vector2Int position) : base(position) { }

        public override TileTypes Type => TileTypes.WIRE;

        public override void AddColor(Direction direction, Color color, int order)
        {
            AddColorToAllWires(color, order);
        }

        public void SetLocked(bool value)
        {
            Locked = value;
            InvokeRedrawing();
        }

        protected override void WriteAdditionalData(TileData data)
        {
            data.Locked = Locked;
        }

        protected override void ApplyAdditionalData(TileData data)
        {
            Locked = data.Locked;
        }
    }
}