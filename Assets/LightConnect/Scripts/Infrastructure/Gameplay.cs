using System;
using Cysharp.Threading.Tasks;
using LightConnect.Core;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Infrastructure
{
    public class Gameplay
    {
        private GameData _gameData;
        private ILevelLoader _levelLoader;
        private LevelView _levelView;

        public Gameplay(LevelView levelView)
        {
            _levelView = levelView;
            _gameData = new GameData();
            _gameData.CurrentLevelId = 15; //***//
            _levelLoader = new StreamingAssetsLevelLoader();
        }

        public async void Run()
        {
            await RunLevel(_gameData.CurrentLevelId);
            _gameData.CurrentLevelId += 1;
            Run();
        }

        public async UniTask RunLevel(int levelNumber)
        {
            var levelData = await _levelLoader.Load(levelNumber);
            var level = new Level();
            level.SetData(levelData);
            LevelRandomizer.Randomize(level.Tiles());
            var levelPresenter = new LevelPresenter(level, _levelView);
            await WaitForEvent(a => level.Win += a, a => level.Win -= a);
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            levelPresenter.Dispose();
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