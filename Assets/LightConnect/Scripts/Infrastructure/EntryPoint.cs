using LightConnect.Audio;
using LightConnect.Model;
using LightConnect.View;
using UnityEngine;
using AudioSettings = LightConnect.Audio.AudioSettings;

namespace LightConnect.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private AudioSettings _audioSettings;

        private GameplayPresenter _gameplayPresenter;

        private void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            Application.targetFrameRate = 60;
            GameMode.Current = GameMode.Mode.GAMEPLAY;

            var gameStateLoader = new PlayerPrefsGameStateLoader();
            var levelLoader = new StreamingAssetsLevelLoader();
            new AudioService(_audioSettings);

            var gameplay = new Gameplay(gameStateLoader, levelLoader);
            _gameplayPresenter = new GameplayPresenter(gameplay, _gameplayView);
            gameplay.Run();
        }

        private void OnDestroy()
        {
            _gameplayPresenter.Dispose();
        }
    }
}