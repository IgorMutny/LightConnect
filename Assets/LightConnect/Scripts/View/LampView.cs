using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Core
{
    public class LampView : TilePartView
    {
        [SerializeField] private Image _aura;

        public override void Initialize(TileViewSettings settings)
        {
            base.Initialize(settings);

            if (_aura != null)
                _aura.gameObject.SetActive(false);
        }

        protected override void Colorize(Model.Color color, bool powered)
        {
            base.Colorize(color, powered);

            _aura.gameObject.SetActive(powered);

            if (powered)
            {
                var unityColor = Settings.Color(color, powered);
                _aura.color = unityColor;
            }
        }
    }
}