using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LightConnect.Core
{
    public class TilePartView : MonoBehaviour
    {
        protected TileViewSettings Settings;
        private Image _image;
        private List<Coroutine> _coroutines = new();

        protected bool IsActive { get; private set; }

        public virtual void Initialize(TileViewSettings settings)
        {
            Settings = settings;
            _image = GetComponent<Image>();
        }

        public virtual void SetActive(bool value)
        {
            IsActive = value;
            gameObject.SetActive(value);
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }


        public void SetColor(Model.Color color, int order)
        {
            SetColor(color, true, order);
        }

        public void SetColor(Model.Color color, bool powered, int order)
        {
            float delay = order * Settings.ColorChangeSpeed;

            if (powered)
            {
                var coroutine = StartCoroutine(StartCororizing(color, powered, delay));
                _coroutines.Add(coroutine);
            }
            else
            {
                Colorize(color, powered);
            }
        }

        public void CancelColor()
        {
            foreach (var coroutine in _coroutines)
                StopCoroutine(coroutine);

            _coroutines.Clear();
        }

        protected virtual void Colorize(Model.Color color, bool powered)
        {
            var unityColor = Settings.Color(color, powered);
            _image.color = unityColor;
        }

        private IEnumerator StartCororizing(Model.Color color, bool powered, float delay)
        {
            yield return new WaitForSeconds(delay);
            Colorize(color, powered);
        }
    }
}