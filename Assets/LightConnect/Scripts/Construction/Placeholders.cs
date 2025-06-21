using System;
using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Construction
{
    public class Placeholders : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePlaceholderPrefab;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private Selection _selection;

        private RectTransform _rect;
        private float _placeholderSize;
        private Dictionary<Vector2Int, TilePlaceholder> _placeholders = new();

        public event Action<Vector2Int> TilePlaceholderClicked;

        public Vector2Int Size => _size;

        public void Initialize()
        {
            _rect = GetComponent<RectTransform>();

            CalculatePlaceholderSize();

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

        private void CalculatePlaceholderSize()
        {
            float width = _rect.sizeDelta.x;
            float height = _rect.sizeDelta.y;
            _placeholderSize = Mathf.Min(width / _size.x, height / _size.y);

            _selection.GetComponent<RectTransform>().sizeDelta = new Vector2(_placeholderSize, _placeholderSize);
        }

        private void CreatePlaceholder(Vector2Int position)
        {
            var go = Instantiate(_tilePlaceholderPrefab, transform);

            var placeholderRect = go.GetComponent<RectTransform>();
            placeholderRect.anchorMin = placeholderRect.anchorMax = Vector2.zero;
            placeholderRect.pivot = Vector2.zero;

            float x = position.x * _placeholderSize;
            float y = position.y * _placeholderSize;

            placeholderRect.anchoredPosition = new Vector2(x, y);
            placeholderRect.sizeDelta = new Vector2(_placeholderSize, _placeholderSize);

            go.name = $"Placeholder {position.x}-{position.y}";

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