using System.Collections.Generic;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Constructor
{
    public class LevelView : MonoBehaviour
    {
        private const float TILE_SIZE = 50f;

        [SerializeField] private GameObject _tilePrefab;

        private List<TileView> _tiles = new();
        private Vector3 _initialPosition;

        public void Initialize()
        {
            _initialPosition = new Vector3(
                transform.position.x - Level.MAX_SIZE * TILE_SIZE / 2,
                transform.position.y - Level.MAX_SIZE * TILE_SIZE / 2,
                0f);
        }

        public TileView AddTile(Vector2Int position)
        {
            float x = _initialPosition.x + position.x * TILE_SIZE;
            float y = _initialPosition.y + position.y * TILE_SIZE;
            Vector3 worldPosition = new Vector3(x, y, 0);
            var tile = Instantiate(_tilePrefab, worldPosition, Quaternion.identity, transform).GetComponent<TileView>();
            tile.gameObject.name = $"Tile {x}-{y}";
            _tiles.Add(tile);
            return tile;
        }
    }
}