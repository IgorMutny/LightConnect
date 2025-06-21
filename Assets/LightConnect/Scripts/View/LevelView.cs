using System.Collections.Generic;
using System.Linq;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.View
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TileViewSettings _settings;
        [SerializeField] private bool _raycastTarget;

        private RectTransform _rect;
        private float _tileSize;
        private Dictionary<Vector2Int, TileView> _tiles = new();

        public Vector2Int InitialPosition { private get; set; }
        public Vector2Int Size { private get; set; }

        public void Initialize()
        {
            _rect = GetComponent<RectTransform>();
            CalculateTileSize();
        }

        public TileView AddTile(TileTypes type, Vector2Int position)
        {
            var prefab = _settings.Prefab(type);
            var go = Instantiate(prefab, transform);

            var tileRect = go.GetComponent<RectTransform>();
            tileRect.anchorMin = tileRect.anchorMax = Vector2.zero;
            tileRect.pivot = Vector2.zero;

            float x = (position.x - InitialPosition.x) * _tileSize;
            float y = (position.y - InitialPosition.y) * _tileSize;

            tileRect.anchoredPosition = new Vector2(x, y);
            tileRect.sizeDelta = new Vector2(_tileSize, _tileSize);

            go.name = $"Tile {position.x}-{position.y}";

            var tile = go.GetComponent<TileView>();
            tile.RaycastTarget = _raycastTarget;
            tile.Initialize(_settings);
            _tiles.Add(position, tile);

            return tile;
        }

        public void RemoveTile(Vector2Int position)
        {
            var tile = _tiles[position];

            if (tile != null)
                Destroy(tile.gameObject);

            _tiles.Remove(position);
        }

        public void Clear()
        {
            foreach (var position in _tiles.Keys.ToList())
                RemoveTile(position);
        }

        private void CalculateTileSize()
        {
            float width = _rect.sizeDelta.x;
            float height = _rect.sizeDelta.y;
            _tileSize = Mathf.Min(width / Size.x, height / Size.y);
        }
    }
}