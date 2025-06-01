using UnityEngine;

namespace LightConnect.Model
{
    public class WarpTile : Tile
    {
        public WarpTile(Vector2Int position) : base(position)
        {
            ConnectedPosition = null;
        }

        public override TileTypes Type => TileTypes.WARP;
        public Vector2Int? ConnectedPosition { get; private set; }

        public void SetConnectedPosition(Vector2Int? connectedPosition)
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