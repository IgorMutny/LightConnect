using System;
using UnityEngine;

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

        public void RotateTo(Quaternion quaternion)
        {
            transform.rotation = quaternion;
        }
    }
}
