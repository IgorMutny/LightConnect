using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Core
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private bool _raycastTarget;

        private float _scale;
        private float _tileRequiredSize;
        private Vector3 _initialPosition;
        private Dictionary<Vector2Int, TileView> _tiles = new();

        public void Initialize()
        {
            CalculateTileValues();
        }

        public TileView AddTile(Vector2Int position)
        {
            float x = _initialPosition.x + position.x * _tileRequiredSize;
            float y = _initialPosition.y + position.y * _tileRequiredSize;
            var worldPosition = new Vector3(x, y, 0);

            var go = Instantiate(_tilePrefab, worldPosition, Quaternion.identity, transform);
            go.transform.localScale = new Vector3(_scale, _scale, 1f);
            go.name = $"Tile {x}-{y}";

            var tile = go.GetComponent<TileView>();
            tile.RaycastTarget = _raycastTarget;
            _tiles.Add(position, tile);
            return tile;
        }

        public void RemoveTile(Vector2Int position)
        {
            var tile = _tiles[position];
            Destroy(tile.gameObject);
            _tiles.Remove(position);
        }

        public void Clear()
        {
            foreach (var position in _tiles.Keys)
                RemoveTile(position);
        }

        private void CalculateTileValues()
        {
            var rect = GetComponent<RectTransform>();
            float width = rect.rect.size.x;
            float height = rect.rect.size.y;

            _tileRequiredSize = Mathf.Min(width / _size.x, height / _size.y);

            var tileRect = _tilePrefab.GetComponent<RectTransform>();
            float tileDefaultSize = tileRect.sizeDelta.x;
            _scale = _tileRequiredSize / tileDefaultSize;

            float initialX = transform.position.x - width / 2 + _tileRequiredSize / 2;
            float initialY = transform.position.y - height / 2 + _tileRequiredSize / 2;
            _initialPosition = new Vector3(initialX, initialY, 0);
        }
    }
}