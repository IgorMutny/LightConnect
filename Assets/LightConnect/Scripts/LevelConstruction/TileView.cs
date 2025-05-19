using System;
using LightConnect.Core;
using LightConnect.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightConnect.LevelConstruction
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TileViewSettings _tileViewSettings;
        [SerializeField] private GameObject _selection;
        [SerializeField] private GameObject[] _wires;
        [SerializeField] private GameObject _element;

        private Image _elementImage;
        private Image[] _wireImages;

        public event Action Clicked;

        public void Initialize()
        {
            _elementImage = _element.GetComponent<Image>();
            _wireImages = new Image[_wires.Length];
            
            for (int i = 0; i < _wires.Length; i++)
                _wireImages[i] = _wires[i].GetComponent<Image>();

            SetSelected(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
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
                var sprite = _tileViewSettings.ElementSprite(type);
                _elementImage.sprite = sprite;
            }
        }

        public void SetWire(bool hasWire, int direction, Model.Color color)
        {
            if (hasWire)
            {
                _wires[direction].SetActive(true);
                _wireImages[direction].color = _tileViewSettings.Color(color, true);
            }
            else
            {
                _wires[direction].SetActive(false);
            }
        }

        public void SetElementColor(Model.Color color, bool powered)
        {
            _elementImage.color = _tileViewSettings.Color(color, powered);
        }
    }
}