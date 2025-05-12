using LightConnect.Core;
using LightConnect.Model;
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
        [SerializeField] private GameObject _wire;
        [SerializeField] private GameObject _element;

        private Subject<Unit> _clicked = new();
        private Image _elementImage;
        private Image _wireImage;
        private Direction _direction;

        public Observable<Unit> Clicked => _clicked;
        public bool IsActive { get; private set; }

        public void Initialize()
        {
            _wire.SetActive(false);
            _element.SetActive(false);
            _wireImage = _wire.GetComponent<Image>();
            _elementImage = _element.GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _clicked.OnNext(Unit.Default);
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

        public void SetElement(ElementTypes type)
        {
            if (type == ElementTypes.NONE)
            {
                _element.SetActive(false);
            }
            else
            {
                _element.SetActive(true);
                var sprite = _allTilesSettings.ElementSettings(type).Sprite;
                _elementImage.sprite = sprite;
            }
        }

        public void SetWire(WireTypes type)
        {
            if (type == WireTypes.NONE)
            {
                _wire.SetActive(false);
            }
            else
            {
                _wire.SetActive(true);
                var sprite = _allTilesSettings.WireSettings(type).Sprite;
                _wireImage.sprite = sprite;
            }
        }

        public void SetColors(Color wireColor, Color elementColor)
        {
            _wireImage.color = wireColor;
            _elementImage.color = elementColor;
        }

        public void SetRotation(Sides side)
        {
            float angle = -(int)side * 90f;
            var rotation = Quaternion.Euler(0, 0, angle);
            _wire.transform.rotation = rotation;
        }
    }
}