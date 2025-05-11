using System.IO;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Meta
{
    public class SaveLoader
    {
        public void Save(Level level, int number)
        {
            var data = new GameData();
            data.LevelNumber = number;
            data.LevelData = level.GetData();

            var json = JsonUtility.ToJson(data);
            string path = Application.streamingAssetsPath + "\\" + number;

            File.WriteAllText(path, json);
        }

        public LevelData Load(int number)
        {
            string path = Application.streamingAssetsPath + "\\" + number;

            string json = File.ReadAllText(path);
            var gameData = JsonUtility.FromJson<GameData>(json);
            return gameData.LevelData;
        }
    }
}