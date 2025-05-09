using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace LightConnect.Core
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        public event Action Clicked;

        public void OnMouseDown()
        {
            Clicked?.Invoke();
        }

        public void SetSprite(Sprite sprite)
        {
            _renderer.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            _renderer.color = color;
        }

        public void RotateTo(float angle)
        {
            var rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;
        }
    }
}
