using System;
using LightConnect.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightConnect.Core
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TileViewSettings _tileViewSettings;
        [SerializeField] private GameObject[] _wires;
        [SerializeField] private GameObject _element;

        private Image _elementImage;
        private Image[] _wireImages;

        public event Action Clicked;

        public bool RaycastTarget
        {
            set
            {
                var images = GetComponentsInChildren<Image>();
                foreach (var image in images)
                    image.raycastTarget = value;
            }
        }

        public void Initialize()
        {
            _elementImage = _element.GetComponent<Image>();
            _wireImages = new Image[_wires.Length];

            for (int i = 0; i < _wires.Length; i++)
                _wireImages[i] = _wires[i].GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }

        public void SetElement(TileTypes type)
        {
            if (type == TileTypes.WIRE)
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