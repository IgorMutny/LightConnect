using System;
using Cysharp.Threading.Tasks;
using LightConnect.Infrastructure;
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

        public event Action<Level> LevelCreated;
        public event Action LevelCompleted;
        public event Action LevelLoaded;
        public event Action LevelLoadingStarted;

        public Gameplay()
        {
            _gameStateLoader = new PlayerPrefsGameStateLoader();
            _gameData = _gameStateLoader.Load();
            _levelLoader = new StreamingAssetsLevelLoader();
        }

        public int CurrentLevelNumber => _gameData.CurrentLevelId;

        public async void Run()
        {
            while (Application.isPlaying)
            {
                await RunLevel(_gameData.CurrentLevelId);
                _gameData.CurrentLevelId += 1;
                _gameStateLoader.Save(_gameData);

                await UniTask.WaitUntil(() => _nextLevelLoadingRequired);
                _nextLevelLoadingRequired = false;
            }
        }

        public void Help()
        {
            _hintHandler?.TryHelp();
        }

        public void RequestLoadNextLevel()
        {
            _nextLevelLoadingRequired = true;
        }

        private async UniTask RunLevel(int levelNumber)
        {
            LevelLoadingStarted?.Invoke();

            var levelData = await _levelLoader.Load(levelNumber);
            var level = new Level();
            level.SetData(levelData);
            _hintHandler = new HintHandler(level);
            LevelRandomizer.Randomize(level);
            LevelCreated?.Invoke(level);

            await UniTask.Delay(TimeSpan.FromSeconds(_levelLoadingDelay));
            LevelLoaded?.Invoke();

            await WaitForEvent(a => level.Win += a, a => level.Win -= a);

            await UniTask.Delay(TimeSpan.FromSeconds(_levelCompletedDelay));
            LevelCompleted?.Invoke();
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
    }
}