using LightConnect.Core;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AllTilesSettings _allTilesSettings;
        [SerializeField] private GameObject _selection;
        [SerializeField] private GameObject _element;

        private Vector2Int _position;
        private Subject<Vector2Int> _clicked = new();
        private Image _elementImage;

        public Observable<Vector2Int> Clicked => _clicked;
        public bool IsActive { get; private set; }

        public void Initialize(Vector2Int position, string elementName)
        {
            _position = position;
            _elementImage = _element.GetComponent<Image>();
            SetElement(elementName);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _clicked.OnNext(_position);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
            IsActive = value;
        }

        public void SetSelected(bool value)
        {
            _selection.SetActive(value);
        }

        public void SetElement(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _element.SetActive(false);
            }
            else
            {
                _element.SetActive(true);
                var sprite = _allTilesSettings.GetTile(name).Sprite;
                _elementImage.sprite = sprite;
            }
        }
    }
}