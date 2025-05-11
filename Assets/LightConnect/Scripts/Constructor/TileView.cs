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
                SetColor(Colors.NONE);
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

        public void SetColor(Colors color)
        {
            switch (color)
            {
                case Colors.NONE: _elementImage.color = Color.white; break;
                case Colors.RED: _elementImage.color = Color.red; break;
                case Colors.GREEN: _elementImage.color = Color.green; break;
                case Colors.BLUE: _elementImage.color = Color.blue; break;
            }
        }

        public void SetRotation(Sides side)
        {
            float angle = -(int)side * 90f;
            var rotation = Quaternion.Euler(0, 0, angle);
            _wire.transform.rotation = rotation;
        }
    }
}