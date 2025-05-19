using System;
using System.IO;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect
{
    public class LevelSaveLoader : ILevelLoader
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

            if (!File.Exists(path))
                throw new Exception($"Level {number} does not exist");

            string json = File.ReadAllText(path);
            LevelData data;

            try
            {
                data = JsonUtility.FromJson<LevelData>(json);
            }
            catch
            {
                throw new Exception($"Level {number} can not be read");
            }

            return data;
        }
    }
}