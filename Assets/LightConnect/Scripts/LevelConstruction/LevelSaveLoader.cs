using System.IO;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.LevelConstruction
{
    public class LevelSaveLoader
    {
        public void Save(Level level, int number)
        {
            var data = level.GetData();
            var json = JsonUtility.ToJson(data);
            string path = Application.streamingAssetsPath + "\\" + number;
            File.WriteAllText(path, json);
        }

        public LevelData Load(int number)
        {
            string path = Application.streamingAssetsPath + "\\" + number;
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<LevelData>(json);
            return data;
        }
    }
}