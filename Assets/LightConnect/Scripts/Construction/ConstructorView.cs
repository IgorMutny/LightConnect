using System;
using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Construction
{
    public class ConstructorView : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePlaceholderPrefab;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private Transform _placeholdersParent;
        [SerializeField] private Selections _selections;

        private float _scale;
        private float _placeholderRequiredSize;
        private Vector3 _initialPosition;
        private List<TilePlaceholder> _placeholders = new();

        public event Action<Vector2Int> TilePlaceholderClicked;

        public Vector2Int Size => _size;

        public void Initialize()
        {
            CalculatePlaceholderValues();

            for (int x = 0; x < _size.x; x++)
                for (int y = 0; y < _size.y; y++)
                    CreatePlaceholder(new Vector2Int(x, y));
        }

        public void Select(Vector2Int position)
        {
            _selections.Select(position);
        }

        private void OnDestroy()
        {
            foreach (var placeholder in _placeholders)
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
            _selections.SetScale(_scale);

            float initialX = transform.position.x - width / 2 + _placeholderRequiredSize / 2;
            float initialY = transform.position.y - height / 2 + _placeholderRequiredSize / 2;
            _initialPosition = new Vector3(initialX, initialY, 0);
        }

        private void CreatePlaceholder(Vector2Int position)
        {
            float x = _initialPosition.x + position.x * _placeholderRequiredSize;
            float y = _initialPosition.y + position.y * _placeholderRequiredSize;
            var worldPosition = new Vector3(x, y, 0);

            var go = Instantiate(_tilePlaceholderPrefab, worldPosition, Quaternion.identity, _placeholdersParent);
            go.transform.localScale = new Vector3(_scale, _scale, 1);

            var placeholder = go.GetComponent<TilePlaceholder>();
            placeholder.Position = position;
            placeholder.Clicked += OnPlaceholderClicked;
            _placeholders.Add(placeholder);

            _selections.CreateSelection(position, worldPosition);
        }

        private void OnPlaceholderClicked(Vector2Int position)
        {
            TilePlaceholderClicked?.Invoke(position);
        }
    }
}