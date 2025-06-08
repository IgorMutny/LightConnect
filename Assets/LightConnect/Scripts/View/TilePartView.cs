using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LightConnect.View
{
    public abstract class TilePartView : MonoBehaviour
    {
        protected TileViewSettings Settings;
        protected Image Image;

        private List<Coroutine> _coroutines = new();

        public virtual void Initialize(TileViewSettings settings)
        {
            Settings = settings;
            Image = GetComponent<Image>();
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

        protected virtual void Colorize(Model.Color color, bool powered)
        {
            var unityColor = Settings.Color(color, powered);
            Image.color = unityColor;
        }

        private IEnumerator StartColorizing(Model.Color color, bool powered, float delay)
        {
            yield return new WaitForSeconds(delay);
            Colorize(color, powered);
        }
    }
}