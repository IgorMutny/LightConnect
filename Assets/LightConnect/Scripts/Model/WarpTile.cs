using UnityEngine;

namespace LightConnect.Model
{
    public class WarpTile : Tile
    {
        public static readonly Vector2Int NONE = new Vector2Int(-1, -1);
        public WarpTile(Vector2Int position) : base(position)
        {
            ConnectedPosition = NONE;
        }

        public override TileTypes Type => TileTypes.WARP;
        public Vector2Int ConnectedPosition { get; private set; }

        public void SetConnectedPosition(Vector2Int connectedPosition)
        {
            ConnectedPosition = connectedPosition;
            InvokeEvaluation();
        }

        protected override void ApplyAdditionalData(TileData data)
        {
            ConnectedPosition = data.ConnectedPosition;
        }

        protected override void WriteAdditionalData(TileData data)
        {
            data.ConnectedPosition = ConnectedPosition;
        }
    }
}