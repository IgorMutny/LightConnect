using System;
using System.Threading;
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
        private bool _nextLevelLoadingRequired;

        public event Action<Level> LevelCreated;
        public event Action ShowWinEffectsRequired;
        public event Action ShowLoadingScreenRequired;
        public event Action HideLoadingScreenRequired;
        public event Action HideWinEffectsRequired;
        public event Action DisposeLevelRequired;

        public Gameplay()
        {
            _gameData = new GameData();
            _gameData.CurrentLevelId = 35; //***//
            _levelLoader = new StreamingAssetsLevelLoader();
        }

        public async void Run()
        {
            while (Application.isPlaying)
            {
                await RunLevel(_gameData.CurrentLevelId);
                _gameData.CurrentLevelId += 1;
                await UniTask.WaitUntil(() => _nextLevelLoadingRequired);
                _nextLevelLoadingRequired = false;
            }
        }

        public void RequestLoadNextLevel()
        {
            _nextLevelLoadingRequired = true;
        }

        private async UniTask RunLevel(int levelNumber)
        {
            HideWinEffectsRequired?.Invoke();
            ShowLoadingScreenRequired?.Invoke();
            DisposeLevelRequired?.Invoke();
            var levelData = await _levelLoader.Load(levelNumber);
            var level = new Level();
            level.SetData(levelData);
            LevelRandomizer.Randomize(level);
            LevelCreated?.Invoke(level);
            await UniTask.Delay(TimeSpan.FromSeconds(_levelLoadingDelay));
            HideLoadingScreenRequired?.Invoke();
            await WaitForEvent(a => level.Win += a, a => level.Win -= a);
            await UniTask.Delay(TimeSpan.FromSeconds(_levelCompletedDelay));
            ShowWinEffectsRequired?.Invoke();
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