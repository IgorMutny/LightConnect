using LightConnect.Core;

namespace LightConnect.Infrastructure
{
    public class Game
    {
        private GameData _gameData;
        private LevelFlow _levelFlow;

        public Game(LevelView levelView)
        {
            _gameData = new GameData();
            _levelFlow = new LevelFlow(levelView);
        }

        public async void Run()
        {
            var levelPassed = await _levelFlow.Run(_gameData.CurrentLevelId);
            if (levelPassed)
            {
                _gameData.CurrentLevelId += 1;
                Run();
            }
        }
    }
}