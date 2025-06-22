using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LightConnect.Audio;
using LightConnect.Infrastructure;
using LightConnect.Tutorial;
using UnityEngine;

namespace LightConnect.Model
{
    public class Gameplay
    {
        private float _levelLoadingDelay = 1f;
        private float _levelCompletedDelay = 1f;

        private GameData _gameData;
        private ILevelLoader _levelLoader;
        private IGameStateLoader _gameStateLoader;
        private bool _nextLevelLoadingRequired;
        private HintHandler _hintHandler;
        private TutorialService _tutorialService;
        private IAdService _adService;

        public event Action<float, float> OptionsInitialized;
        public event Action LevelLoadingStarted;
        public event Action<Level> LevelCreated;
        public event Action LevelReady;
        public event Action<TutorialMessage> TutorialRequired;
        public event Action LevelWon;
        public event Action LevelFinished;

        public Gameplay(IGameStateLoader gameStateLoader, ILevelLoader levelLoader, TutorialService tutorialService, IAdService adService)
        {
            _gameStateLoader = gameStateLoader;
            _gameData = _gameStateLoader.Load();
            _levelLoader = levelLoader;
            _tutorialService = tutorialService;
            _adService = adService;
        }

        public int CurrentLevelId => _gameData.CurrentLevelId;

        public async void Run()
        {
            InitializeOptions();

            while (Application.isPlaying)
            {
                await RunLevel(_gameData.CurrentLevelId);

                _gameData.CurrentLevelId += 1;
                _gameStateLoader.Save(_gameData);

                await UniTask.WaitUntil(() => _nextLevelLoadingRequired);
                _nextLevelLoadingRequired = false;

                await _adService.ShowInterstitialsIfAllowed();

                LevelFinished?.Invoke();

            }
        }

        public async Task Help()
        {
            if (_hintHandler.HasWrongOrientatedTiles())
            {
                var result = await _adService.ShowRewarded();

                if (result)
                    _hintHandler.Help();
            }
        }

        public void RequestLoadNextLevel()
        {
            _nextLevelLoadingRequired = true;
        }

        public void SetSoundVolume(float value)
        {
            _gameData.SoundVolume = value;
            _gameStateLoader.Save(_gameData);
            AudioService.Instance.SetSoundVolume(value);
        }

        public void SetMusicVolume(float value)
        {
            _gameData.MusicVolume = value;
            _gameStateLoader.Save(_gameData);
            AudioService.Instance.SetMusicVolume(value);
        }

        private async UniTask RunLevel(int levelId)
        {
            LevelLoadingStarted?.Invoke();

            var levelData = await _levelLoader.Load(levelId);
            var level = new Level();
            level.SetData(levelData);
            _hintHandler = new HintHandler(level);
            LevelRandomizer.Randomize(level);

            LevelCreated?.Invoke(level);

            await UniTask.Delay(TimeSpan.FromSeconds(_levelLoadingDelay));

            LevelReady?.Invoke();

            if (_tutorialService.GetMessageForLevel(levelId, out TutorialMessage message))
                TutorialRequired?.Invoke(message);

            await WaitForEvent(a => level.Win += a, a => level.Win -= a);

            await UniTask.Delay(TimeSpan.FromSeconds(_levelCompletedDelay));

            LevelWon?.Invoke();
            _hintHandler = null;
        }

        private async UniTask WaitForEvent(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            var tcs = new UniTaskCompletionSource();

            void OnEvent()
            {
                unsubscribe(OnEvent);
                tcs.TrySetResult();
            }

            subscribe(OnEvent);
            await tcs.Task;
        }

        private void InitializeOptions()
        {
            OptionsInitialized?.Invoke(_gameData.SoundVolume, _gameData.MusicVolume);

            AudioService.Instance.SetSoundVolume(_gameData.SoundVolume);
            AudioService.Instance.SetMusicVolume(_gameData.MusicVolume);
        }
    }
}