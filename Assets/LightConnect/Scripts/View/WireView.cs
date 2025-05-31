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
        private bool _isActive;

        public override void Initialize(TileViewSettings settings)
        {
            base.Initialize(settings);
            _aura.gameObject.SetActive(false);
        }

        public void SetActive(bool value)
        {
            _isActive = value;
            gameObject.SetActive(_isActive);
            _aura.gameObject.SetActive(_isActive && _isColored);
            _lock.SetActive(_isActive && _isLocked);
        }

        public void SetLocked(bool value)
        {
            _isLocked = value;
            _lock.SetActive(_isActive && _isLocked);
        }

        protected override void Colorize(Model.Color color, bool powered)
        {
            base.Colorize(color, powered);

            _isColored = color != Model.Color.None;
            _aura.gameObject.SetActive(_isActive && _isColored);

            if (_isColored)
            {
                var unityColor = Settings.Color(color, powered);
                _aura.color = unityColor;
            }
        }
    }
}