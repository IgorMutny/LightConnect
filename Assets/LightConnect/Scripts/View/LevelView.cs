using System.Collections.Generic;
using System.Linq;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.View
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private int _defaultSize;
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;
        [SerializeField] private TileViewSettings _settings;
        [SerializeField] private GameObject _confetti;

        private Dictionary<Vector2Int, TileView> _tiles = new();

        public Vector2Int InitialPosition { private get; set; }
        public Vector2Int Size { private get; set; }

        public void Initialize()
        {
            float scale = (float)_defaultSize / Mathf.Max(Size.x, Size.y);
            scale = Mathf.Clamp(scale, _minScale, _maxScale);
            transform.localScale = new Vector3(scale, scale, 1);
        }

        public TileView AddTile(TileTypes type, Vector2Int position)
        {
            float x = position.x - InitialPosition.x - (float)Size.x / 2 + 0.5f;
            float y = position.y - InitialPosition.y - (float)Size.y / 2 + 0.5f;

            var prefab = _settings.Prefab(type);
            var go = Instantiate(prefab, transform);
            go.name = $"Tile {position.x}-{position.y}";
            go.transform.localPosition = new Vector3(x, y, 0);

            var tile = go.GetComponent<TileView>();
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

        public void SetConfettiActive(bool value)
        {
            _confetti?.SetActive(value);
        }
    }
}