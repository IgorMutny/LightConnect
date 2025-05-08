using UnityEngine;

namespace LightConnect.Core
{
    public class Map
    {
        public readonly Vector2Int Size;
        private readonly Tile[,] _tiles;

        public Map(Vector2Int size)
        {
            Size = size;
            _tiles = new Tile[size.x, size.y];
        }

        public Tile[,] Tiles => _tiles;

        public void SetTile(Tile tile, Vector2Int position)
        {
            _tiles[position.x, position.y] = tile;
        }
    }
}