using System;
using Cysharp.Threading.Tasks;
using LightConnect.Model;

namespace LightConnect.Core
{
    public class Game
    {
        private ILevelLoader _levelLoader;
        private int _currentLevelNumber = 0;
        private Gameplay _gameplay;

        public Game()
        {
            _levelLoader = new LevelSaveLoader();
        }

        public LevelView LevelView { get; set; }

        public void CreateGameplay()
        {
            LevelData levelData;
            try
            {
                levelData = _levelLoader.Load(_currentLevelNumber);
            }
            catch
            {
                throw new Exception("Can not load next level");
            }

            _gameplay = new Gameplay(levelData, LevelView);
            _gameplay.IsWon += OnLevelWon;
        }

        private void OnLevelWon()
        {
            _gameplay.IsWon -= OnLevelWon;

            _ = Wait();
        }

        private async UniTask Wait()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3f));

            _gameplay.Dispose();
            _currentLevelNumber += 1;

            CreateGameplay();
        }
    }
}