using System;
using LightConnect.Model;

namespace LightConnect.Core
{
    public class Gameplay
    {
        private Level _level;
        private LevelPresenter _levelPresenter;
        private LevelView _levelView;

        public event Action IsWon;

        public Gameplay(LevelData levelData, LevelView levelView)
        {
            _level = new Level();
            _level.SetData(levelData);
            _level.Randomize();
            _levelView = levelView;
            _levelPresenter = new LevelPresenter(_level, _levelView);
            _level.IsWon += OnLevelWon;
        }

        public void Dispose()
        {
            _level.IsWon -= OnLevelWon;
            _levelPresenter.Dispose();
            _levelView.Clear();
        }

        private void OnLevelWon()
        {
            IsWon?.Invoke();
        }
    }
}