#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Construction
{
    public class PlaceholdersView : MonoBehaviour
    {
        [SerializeField] private int _defaultSize;
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;
        [SerializeField] private GameObject _placeholderPrefab;
        [SerializeField, Range(1, 16)] private int _dimensionSize;
        [SerializeField] private Selection _selection;

        private Vector2Int _size;
        private Dictionary<Vector2Int, TilePlaceholder> _placeholders = new();

        public event Action<Vector2Int> TilePlaceholderClicked;

        public int DimensionSize => _dimensionSize;

        public void Initialize()
        {
            _size = new Vector2Int(_dimensionSize, _dimensionSize);

            float scale = (float)_defaultSize / Mathf.Max(_size.x, _size.y);
            scale = Mathf.Clamp(scale, _minScale, _maxScale);
            transform.localScale = new Vector3(scale, scale, 1);
            _selection.transform.localScale = transform.localScale;

            for (int x = 0; x < _size.x; x++)
                for (int y = 0; y < _size.y; y++)
                    CreatePlaceholder(new Vector2Int(x, y));

            Select(Vector2Int.zero);
        }

        public void Select(Vector2Int position)
        {
            var worldPosition = _placeholders[position].transform.position;
            _selection.SetPosition(worldPosition);
        }

        private void OnDestroy()
        {
            foreach (var placeholder in _placeholders.Values)
                placeholder.Clicked -= OnPlaceholderClicked;
        }

        private void CreatePlaceholder(Vector2Int position)
        {
            float x = position.x - (float)_size.x / 2 + 0.5f;
            float y = position.y - (float)_size.y / 2 + 0.5f;

            var go = Instantiate(_placeholderPrefab, transform);
            go.name = $"Placeholder {position.x}-{position.y}";
            go.transform.localPosition = new Vector3(x, y, 0);

            var placeholder = go.GetComponent<TilePlaceholder>();
            placeholder.Position = position;
            placeholder.Clicked += OnPlaceholderClicked;
            placeholder.Initialize();
            _placeholders.Add(position, placeholder);
        }

        private void OnPlaceholderClicked(Vector2Int position)
        {
            TilePlaceholderClicked?.Invoke(position);
        }
    }
}

#endif