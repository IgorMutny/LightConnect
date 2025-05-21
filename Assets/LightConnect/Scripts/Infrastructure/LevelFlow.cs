using System;
using Cysharp.Threading.Tasks;
using LightConnect.Core;
using LightConnect.Model;

namespace LightConnect.Infrastructure
{
    public class LevelFlow
    {
        private LevelView _levelView;
        private ILevelLoader _levelLoader;

        public LevelFlow(LevelView levelView)
        {
            _levelLoader = new StreamingAssetsLevelLoader();
            _levelView = levelView;
        }

        public async UniTask<bool> Run(int levelNumber)
        {
            var levelData = await _levelLoader.Load(levelNumber);
            var level = new Level();
            level.SetData(levelData);
            LevelRandomizer.Randomize(level.Tiles());

            var levelPresenter = new LevelPresenter(level, _levelView);

            await UniTask.WaitUntil(() => level.IsWon);

            levelPresenter.Dispose();

            await UniTask.Delay(TimeSpan.FromSeconds(3f));

            _levelView.Clear();

            return true;
        }
    }
}