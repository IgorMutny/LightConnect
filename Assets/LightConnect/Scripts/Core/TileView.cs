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

        public void SetPower(bool powered)
        {
            if (powered)
                _renderer.color = Color.red;
            else
                _renderer.color = Color.white;
        }

        public void RotateTo(float angle)
        {
            var rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;
        }
    }
}
