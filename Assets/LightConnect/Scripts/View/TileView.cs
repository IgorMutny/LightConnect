using System;
using LightConnect.Audio;
using LightConnect.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightConnect.View
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TileBasementView _basement;
        [SerializeField] private TilePartView _element;
        [SerializeField] private WireSetView _wireSet;

        private TileViewSettings _settings;

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

        public void Initialize(TileViewSettings settings)
        {
            _settings = settings;
            _element?.Initialize(_settings);
            _basement?.Initialize(_settings);
            _wireSet?.Initialize(_settings);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }

        public void SetWireSet(WireSetTypes type)
        {
            _wireSet?.SetWireSet(type);
        }

        public void SetOrientation(Direction orientation, bool soundsAllowed)
        {
            _wireSet?.SetOrientation(orientation);

            if (soundsAllowed)
                AudioService.Instance?.PlayClickSound();
        }

        public void SetBasementColor(TileBasementView.Color color, bool powered, int order)
        {
            _basement?.SetColor(color, powered, order);
        }

        public void SetElementColor(Model.Color color, bool powered, int order)
        {
            _element?.SetColor(color, powered, order);
        }

        public void SetWireColor(Direction direction, Model.Color color, int order)
        {
            _wireSet?.SetColor(direction, color, order);
        }

        public void SetLocked(bool value)
        {
            _wireSet?.SetLocked(value);
        }

        public void CancelColor()
        {
            _basement?.CancelColor();
            _element?.CancelColor();
            _wireSet?.CancelColor();
        }
    }
}