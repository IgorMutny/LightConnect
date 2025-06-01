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

        private float _scale;
        private float _placeholderRequiredSize;
        private Vector3 _initialWorldPosition;
        private Dictionary<Vector2Int, TilePlaceholder> _placeholders = new();

        public event Action<Vector2Int> TilePlaceholderClicked;

        public Vector2Int Size => _size;

        public void Initialize()
        {
            CalculatePlaceholderValues();

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

        private void CalculatePlaceholderValues()
        {
            var rect = GetComponent<RectTransform>();
            float width = rect.rect.size.x;
            float height = rect.rect.size.y;

            _placeholderRequiredSize = Mathf.Min(width / _size.x, height / _size.y);

            var placeholderRect = _tilePlaceholderPrefab.GetComponent<RectTransform>();
            float placeholderDefaultSize = placeholderRect.sizeDelta.x;
            _scale = _placeholderRequiredSize / placeholderDefaultSize;
            _selection.SetScale(_scale);

            int maxDimension = Mathf.Max(Size.x, Size.y);
            float initialX = transform.position.x - (width * Size.x / maxDimension - _placeholderRequiredSize) / 2;
            float initialY = transform.position.y - (height * Size.y / maxDimension - _placeholderRequiredSize) / 2;
            _initialWorldPosition = new Vector3(initialX, initialY, 0);
        }

        private void CreatePlaceholder(Vector2Int position)
        {
            float x = _initialWorldPosition.x + position.x * _placeholderRequiredSize;
            float y = _initialWorldPosition.y + position.y * _placeholderRequiredSize;
            var worldPosition = new Vector3(x, y, 0);

            var go = Instantiate(_tilePlaceholderPrefab, worldPosition, Quaternion.identity, transform);
            go.transform.localScale = new Vector3(_scale, _scale, 1);
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