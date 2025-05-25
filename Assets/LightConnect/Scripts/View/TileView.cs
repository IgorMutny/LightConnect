using System;
using LightConnect.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightConnect.Core
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TileViewSettings _settings;
        [SerializeField] private TilePartView _element;
        [SerializeField] private TilePartView _wireSetCenter;
        [SerializeField] private TilePartView[] _wires;

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
            _element.Initialize(_settings);
            _wireSetCenter.Initialize(_settings);

            for (int i = 0; i < _wires.Length; i++)
                _wires[i].Initialize(_settings);
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
                var sprite = _settings.ElementSprite(type);
                _element.SetSprite(sprite);
            }
        }

        public void SetElement(TileTypes type, Model.Color color)
        {
            SetElement(type);
            _element.SetColor(color, false, 0);
        }

        public void SetWireSet(WireSetTypes type, Direction orientation)
        {
            if (type != WireSetTypes.NONE)
            {
                _wireSetCenter.SetActive(true);
                var sprite = _settings.WireSetCenterSprite(type);
                _wireSetCenter.SetSprite(sprite);
                var rotation = Quaternion.Euler(0, 0, -(int)orientation * 90);
                _wireSetCenter.transform.rotation = rotation;
            }
            else
            {
                _wireSetCenter.SetActive(false);
            }
        }

        public void SetWire(bool hasWire, int direction)
        {
            _wires[direction].SetActive(hasWire);
            _wires[direction].SetColor(Model.Color.None, false, 0);
        }

        public void SetElementColor(Model.Color color, bool powered, int order)
        {
            _element.SetColor(color, powered, order);
        }

        public void SetWireColor(int direction, Model.Color color, int order)
        {
            _wires[direction].SetColor(color, true, order);
        }

        public void SetWireSetCenterColor(Model.Color color, int order)
        {
            _wireSetCenter.SetColor(color, true, order);
        }

        public void StopColorCoroutines()
        {
            _element.StopColorCoroutines();
            _wireSetCenter.StopColorCoroutines();

            for (int i = 0; i < _wires.Length; i++)
                _wires[i].StopColorCoroutines();
        }
    }
}