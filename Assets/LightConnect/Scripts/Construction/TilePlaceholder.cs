using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightConnect.Construction
{
    public class TilePlaceholder : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Color _evenColor;
        [SerializeField] private Color _oddColor;

        public event Action<Vector2Int> Clicked;
        public Vector2Int Position { get; set; }

        public void Initialize()
        {
            var image = GetComponent<Image>();

            if ((Position.x + Position.y) % 2 == 0)
                image.color = _evenColor;
            else
                image.color = _oddColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(Position);
        }
    }
}