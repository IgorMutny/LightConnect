using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LightConnect.View
{
    public class OptionsScreen : MonoBehaviour, IPointerClickHandler
    {
        public event Action CloseRequired;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject == gameObject)
            {
                CloseRequired?.Invoke();
            }
        }
    }
}