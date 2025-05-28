using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Core
{
    public class WireView : TilePartView
    {
        [SerializeField] private Image _aura;
        [SerializeField] private GameObject _lock;

        private bool _isLocked;
        private bool _isColored;

        public override void Initialize(TileViewSettings settings)
        {
            base.Initialize(settings);
            _aura.gameObject.SetActive(false);
        }

        public override void SetActive(bool value)
        {
            base.SetActive(value);
            _aura.gameObject.SetActive(IsActive && _isColored);
            _lock.SetActive(IsActive && _isLocked);
        }

        public void SetLocked(bool value)
        {
            _isLocked = value;
            _lock.SetActive(IsActive && _isLocked);
        }

        protected override void Colorize(Model.Color color, bool powered)
        {
            base.Colorize(color, powered);

            _isColored = color != Model.Color.None;
            _aura.gameObject.SetActive(IsActive && _isColored);

            if (_isColored)
            {
                var unityColor = Settings.Color(color, powered);
                _aura.color = unityColor;
            }
        }
    }
}