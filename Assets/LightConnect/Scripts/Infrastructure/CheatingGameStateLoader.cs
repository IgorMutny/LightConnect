namespace LightConnect.Infrastructure
{
    public class CheatingGameStateLoader : IGameStateLoader
    {
        private const int REQUIRED_LEVEL_ID = 42;

        public GameData Load()
        {
            var data = new GameData();
            data.CurrentLevelId = REQUIRED_LEVEL_ID;
            return data;
        }

        public void Save(GameData data) { }
    }
}