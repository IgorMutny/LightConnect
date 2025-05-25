using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LightConnect.Core
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private bool _raycastTarget;

        private float _scale;
        private float _tileRequiredSize;
        private Vector3 _initialWorldPosition;
        private Dictionary<Vector2Int, TileView> _tiles = new();

        public Vector2Int Size { private get; set; }
        public Vector2Int InitialPosition { private get; set; }

        public void Initialize()
        {
            CalculateTileValues();
        }

        public TileView AddTile(Vector2Int position)
        {
            float x = _initialWorldPosition.x + (position.x - InitialPosition.x) * _tileRequiredSize;
            float y = _initialWorldPosition.y + (position.y - InitialPosition.y) * _tileRequiredSize;
            var worldPosition = new Vector3(x, y, 0);

            var go = Instantiate(_tilePrefab, worldPosition, Quaternion.identity, transform);
            go.transform.localScale = new Vector3(_scale, _scale, 1f);
            go.name = $"Tile {position.x}-{position.y}";

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
            foreach (var position in _tiles.Keys.ToList())
                RemoveTile(position);
        }

        private void CalculateTileValues()
        {
            var rect = GetComponent<RectTransform>();
            float width = rect.rect.size.x;
            float height = rect.rect.size.y;

            _tileRequiredSize = Mathf.Min(width / Size.x, height / Size.y);

            var tileRect = _tilePrefab.GetComponent<RectTransform>();
            float tileDefaultSize = tileRect.rect.size.x;
            _scale = _tileRequiredSize / tileDefaultSize;

            int maxDimension = Mathf.Max(Size.x, Size.y);
            float initialX = transform.position.x - (width * Size.x / maxDimension - _tileRequiredSize) / 2;
            float initialY = transform.position.y - (height * Size.y / maxDimension - _tileRequiredSize) / 2;
            _initialWorldPosition = new Vector3(initialX, initialY, 0);
        }
    }
}