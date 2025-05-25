using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace LightConnect.Core
{
    public class TilePartView : MonoBehaviour
    {
        [SerializeField] private Image _aura;
        [SerializeField] private Image _image;

        private TileViewSettings _settings;
        private List<Coroutine> _colorCoroutines = new();

        public void Initialize(TileViewSettings settings)
        {
            _settings = settings;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void SetColor(Model.Color color, bool powered, int order)
        {
            if (_aura != null)
                _aura.gameObject.SetActive(false);

            if (!gameObject.activeInHierarchy)
                return;

            if (order == 0 || !powered)
            {
                Colorize(color, powered);
            }
            else
            {
                var coroutine = StartCoroutine(SetColorCoroutine(color, powered, order));
                _colorCoroutines.Add(coroutine);
            }
        }

        public void StopColorCoroutines()
        {
            foreach (var coroutine in _colorCoroutines)
                StopCoroutine(coroutine);

            _colorCoroutines.Clear();
        }

        private IEnumerator SetColorCoroutine(Model.Color color, bool powered, int order)
        {
            yield return new WaitForSeconds(order * _settings.ColorChangeSpeed);
            Colorize(color, powered);
        }

        private void Colorize(Model.Color color, bool powered)
        {
            _image.color = _settings.Color(color, powered);

            if (_aura != null)
            {
                _aura.gameObject.SetActive(powered && color != Model.Color.None);
                _aura.color = _settings.Color(color, powered);
            }
        }
    }
}