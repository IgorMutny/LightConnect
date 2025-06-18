using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LightConnect.View
{
    public class OptionsScreen : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject == gameObject)
            {
                Hide();
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}