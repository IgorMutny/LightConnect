using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LightConnect.Construction
{
    public class TilePlaceholder : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Vector2Int> Clicked;
        public Vector2Int Position { get; set; }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(Position);
        }
    }
}