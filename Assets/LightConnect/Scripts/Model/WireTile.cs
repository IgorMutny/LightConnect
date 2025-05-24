using UnityEngine;

namespace LightConnect.Model
{
    public class WireTile : Tile, IRotatableTile
    {
        public WireTile(Vector2Int position) : base(position) { }

        public override TileTypes Type => TileTypes.WIRE;

        public override void AddColor(Direction direction, Color color)
        {
            WireSet.AddColorToAllWires(color);
            InvokeRecolorized();
        }
    }
}