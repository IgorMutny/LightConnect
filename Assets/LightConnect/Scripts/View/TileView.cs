using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float _colorChangeSpeed;

        private Image _elementImage;
        private Image[] _wireImages;
        private List<Coroutine> _colorCoroutines = new();

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

        public void SetElement(TileTypes type, Model.Color color)
        {
            SetElement(type);
            _elementImage.color = _tileViewSettings.Color(color, false);
        }

        public void SetWire(bool hasWire, int direction)
        {
            _wires[direction].SetActive(hasWire);
            _wireImages[direction].color = _tileViewSettings.Color(Model.Color.None, false);
        }

        public void SetElementColor(Model.Color color, bool powered, int orderInPowerChain)
        {
            var coroutine = StartCoroutine(SetColorCoroutine(_elementImage, color, powered, orderInPowerChain));
            _colorCoroutines.Add(coroutine);
        }

        public void SetWireColor(int direction, Model.Color color, int orderInPowerChain)
        {
            var coroutine = StartCoroutine(SetColorCoroutine(_wireImages[direction], color, true, orderInPowerChain));
            _colorCoroutines.Add(coroutine);
        }

        public void StopColorCoroutines()
        {
            foreach (var coroutine in _colorCoroutines)
                StopCoroutine(coroutine);

            _colorCoroutines.Clear();
        }

        private IEnumerator SetColorCoroutine(Image image, Model.Color color, bool powered, int order)
        {
            yield return new WaitForSeconds(order * _colorChangeSpeed);
            image.color = _tileViewSettings.Color(color, powered);
        }
    }
}