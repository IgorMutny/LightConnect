using UnityEngine;

namespace LightConnect.Infrastructure
{
    public class PlayerPrefsGameStateLoader : IGameStateLoader
    {
        private const string CURRENT_LEVEL_ID_KEY = "CurrentLevelId";
        private const string SOUND_VOLUME_KEY = "SoundVolume";
        private const string MUSIC_VOLUME_KEY = "MusicVolume";

        public GameData Load()
        {
            var data = new GameData();

            if (PlayerPrefs.HasKey(CURRENT_LEVEL_ID_KEY))
                data.CurrentLevelId = PlayerPrefs.GetInt(CURRENT_LEVEL_ID_KEY);
            else
                data.CurrentLevelId = 1;

            if (PlayerPrefs.HasKey(SOUND_VOLUME_KEY))
                data.SoundVolume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY);
            else
                data.SoundVolume = 0.5f;

            if (PlayerPrefs.HasKey(MUSIC_VOLUME_KEY))
                data.MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
            else
                data.MusicVolume = 0.5f;

            return data;
        }

        public void Save(GameData data)
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL_ID_KEY, data.CurrentLevelId);
            PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, data.SoundVolume);
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, data.MusicVolume);
        }
    }
}