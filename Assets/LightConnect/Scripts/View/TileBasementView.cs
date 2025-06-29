using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LightConnect.View
{
    public class TileBasementView : MonoBehaviour
    {
        protected TileViewSettings Settings;
        private SpriteRenderer _renderer;
        private List<Coroutine> _coroutines = new();

        public virtual void Initialize(TileViewSettings settings)
        {
            Settings = settings;
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetColor(Color color, bool powered, int order)
        {
            float delay = order * Settings.ColorChangeSpeed;

            if (powered)
            {
                var coroutine = StartCoroutine(StartColorizing(color, powered, delay));
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

        protected virtual void Colorize(Color color, bool powered)
        {
            var unityColor = Settings.BasementColor(color, powered);
            _renderer.color = unityColor;
        }

        private IEnumerator StartColorizing(Color color, bool powered, float delay)
        {
            yield return new WaitForSeconds(delay);
            Colorize(color, powered);
        }

        public enum Color
        {
            GRAY = 0,
            ODD = 1,
            EVEN = 2,
            WARP_CONNECTED = 3,
        }
    }
}