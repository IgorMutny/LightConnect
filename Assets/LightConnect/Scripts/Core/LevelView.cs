using System.Collections.Generic;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Core
{
    public class LevelView : MonoBehaviour
    {
        private const float TILE_SIZE = 50f;

        [SerializeField] private GameObject _tilePrefab;

        private List<TileView> _tiles = new();
        private Vector3 _initialPosition;
        private float _scale;

        public void Initialize(Vector2Int size)
        {
            _initialPosition = new Vector3(
                transform.position.x - Level.MAX_SIZE * TILE_SIZE / 2,
                transform.position.y - Level.MAX_SIZE * TILE_SIZE / 2,
                0f);

            _scale = Mathf.Min((float)Level.MAX_SIZE / size.x, (float)Level.MAX_SIZE / size.y);
        }

        public TileView AddTile(Vector2Int position)
        {
            float x = _initialPosition.x + (position.x + 0.5f) * TILE_SIZE * _scale;
            float y = _initialPosition.y + (position.y + 0.5f) * TILE_SIZE * _scale;
            Vector3 worldPosition = new Vector3(x, y, 0);
            var tile = Instantiate(_tilePrefab, worldPosition, Quaternion.identity, transform).GetComponent<TileView>();
            tile.transform.localScale = new Vector3(_scale, _scale, 1f);
            tile.gameObject.name = $"Tile {x}-{y}";
            _tiles.Add(tile);
            return tile;
        }

        public void Clear()
        {
            foreach (var tile in _tiles)
                if (tile != null && tile.gameObject != null)
                    Destroy(tile.gameObject);

            _tiles.Clear();
        }
    }
}