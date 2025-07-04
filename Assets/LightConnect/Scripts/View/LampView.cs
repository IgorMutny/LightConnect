using LightConnect.Audio;
using UnityEngine;

namespace LightConnect.View
{
    public class LampView : TilePartView
    {
        [SerializeField] private SpriteRenderer _aura;

        private bool _cachedPowered;

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

            if (_cachedPowered != powered)
            {
                if (powered)
                    AudioService.Instance?.PlayLampSound();

                _cachedPowered = powered;
            }
        }
    }
}