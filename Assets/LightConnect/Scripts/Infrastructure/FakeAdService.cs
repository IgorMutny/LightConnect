using System;
using Cysharp.Threading.Tasks;
using LightConnect.Audio;
using LightConnect.UI;

namespace LightConnect.Infrastructure
{
    public class FakeAdService : IAdService
    {
        private TimeSpan _minDelayBetweenInterstitials = TimeSpan.FromSeconds(120);
        private TimeSpan _fakeAdDelay = TimeSpan.FromSeconds(2);
        private DateTime _lastTimeInterstitialsShown; 

        private UIView _uiView;

        public FakeAdService(UIView uiView)
        {
            _uiView = uiView;
            _uiView.HideAdScreen();
            _lastTimeInterstitialsShown = DateTime.Now;
        }

        public async UniTask ShowInterstitialsIfAllowed()
        {
            var currentDateTime = DateTime.Now;

            if ((currentDateTime - _lastTimeInterstitialsShown) < _minDelayBetweenInterstitials)
                return;

            AudioService.Instance.Pause();
            _uiView.ShowAdScreen();
            await UniTask.Delay(_fakeAdDelay);

            _lastTimeInterstitialsShown = DateTime.Now;
            _uiView.HideAdScreen();
            AudioService.Instance.Resume();
        }

        public async UniTask<bool> ShowRewarded()
        {
            AudioService.Instance.Pause();
            _uiView.ShowAdScreen();
            await UniTask.Delay(_fakeAdDelay);
            _uiView.HideAdScreen();
            AudioService.Instance.Resume();
            return true;
        }
    }
}