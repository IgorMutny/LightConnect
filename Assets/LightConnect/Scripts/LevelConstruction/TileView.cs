using LightConnect.Core;
using LightConnect.Model;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightConnect.LevelConstruction
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AllTilesSettings _allTilesSettings;
        [SerializeField] private GameObject _selection;
        [SerializeField] private GameObject[] _wires;
        [SerializeField] private GameObject _element;

        private Subject<Unit> _clicked = new();
        private Image _elementImage;
        private Image[] _wireImages;

        public Observable<Unit> Clicked => _clicked;
        public bool IsActive { get; private set; }

        public void Initialize()
        {
            _elementImage = _element.GetComponent<Image>();
            _wireImages = new Image[_wires.Length];
            for (int i = 0; i < _wires.Length; i++)
                _wireImages[i] = _wires[i].GetComponent<Image>();
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

        public void SetWire(bool hasWire, int direction, Model.Color color)
        {
            if (hasWire)
            {
                _wires[direction].SetActive(true);
                _wireImages[direction].color = ColorDictionary.GetColor(color, true);
            }
            else
            {
                _wires[direction].SetActive(false);
            }
        }

        public void SetElementColor(Model.Color elementColor, bool powered)
        {
            _elementImage.color = ColorDictionary.GetColor(elementColor, powered);
        }
    }
}