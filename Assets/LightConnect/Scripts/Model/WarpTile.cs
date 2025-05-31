using UnityEngine;

namespace LightConnect.Model
{
    public class WarpTile : Tile
    {
        private static readonly Vector2Int NONE = new Vector2Int(-1, -1);

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
            if (data.ConnectedPosition == NONE)
                ConnectedPosition = null;
            else
                ConnectedPosition = data.ConnectedPosition;
        }

        protected override void WriteAdditionalData(TileData data)
        {
            if (ConnectedPosition.HasValue)
                data.ConnectedPosition = ConnectedPosition.Value;
            else
                data.ConnectedPosition = NONE;
        }
    }
}