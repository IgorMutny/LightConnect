using R3;
using UnityEngine;

namespace LightConnect.Core
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        private Subject<Unit> _clicked = new();

        public Observable<Unit> Clicked => _clicked;

        public void OnMouseDown()
        {
            _clicked.OnNext(Unit.Default);
        }

        public void SetSprite(Sprite sprite)
        {
            _renderer.sprite = sprite;
        }

        public void SetColors(Color wireColor, Color elementColor)
        {
            _renderer.color = wireColor;
        }

        public void RotateTo(float angle)
        {
            var rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;
        }
    }
}
