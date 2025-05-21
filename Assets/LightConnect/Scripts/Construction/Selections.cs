using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Construction
{
    public class Selections : MonoBehaviour
    {
        [SerializeField] private GameObject _selectionPrefab;

        private float _scale;
        private Dictionary<Vector2Int, GameObject> _selections = new();

        public void SetScale(float scale)
        {
            _scale = scale;
        }

        public void CreateSelection(Vector2Int position, Vector3 worldPosition)
        {
            var selection = Instantiate(_selectionPrefab, worldPosition, Quaternion.identity, transform);
            selection.transform.localScale = new Vector3(_scale, _scale, 1);
            selection.SetActive(false);
            _selections.Add(position, selection);
        }

        public void Select(Vector2Int selectedPosition)
        {
            foreach ((var position, var selection) in _selections)
                selection.SetActive(position == selectedPosition);
        }
    }
}