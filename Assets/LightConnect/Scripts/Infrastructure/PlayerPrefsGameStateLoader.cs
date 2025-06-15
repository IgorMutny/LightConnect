using UnityEngine;

namespace LightConnect.Infrastructure
{
    public class PlayerPrefsGameStateLoader : IGameStateLoader
    {
        private const string CURRENT_LEVEL_ID_KEY = "CurrentLevelId";

        public GameData Load()
        {
            var data = new GameData();

            if (PlayerPrefs.HasKey(CURRENT_LEVEL_ID_KEY))
                data.CurrentLevelId = PlayerPrefs.GetInt(CURRENT_LEVEL_ID_KEY);
            else
                data.CurrentLevelId = 1;

            return data;
        }

        public void Save(GameData data)
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL_ID_KEY, data.CurrentLevelId);
        }
    }
}