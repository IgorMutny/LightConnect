using System;
using System.IO;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.LevelConstruction
{
    public class LevelSaveLoader
    {
        public void Save(Level level, int id)
        {
            var data = level.GetData();
            var json = JsonUtility.ToJson(data);
            string path = Application.streamingAssetsPath + "\\" + id;
            File.WriteAllText(path, json);
        }

        public LevelData Load(int id)
        {
            string path = Application.streamingAssetsPath + "\\" + id;

            if (!File.Exists(path))
                throw new Exception($"Level {id} does not exist");

            string json = File.ReadAllText(path);
            LevelData data;

            try
            {
                data = JsonUtility.FromJson<LevelData>(json);
            }
            catch
            {
                throw new Exception($"Level {id} can not be read");
            }

            return data;
        }

        public bool LevelExists(int id)
        {
            string path = Application.streamingAssetsPath + "\\" + id;
            return File.Exists(path);
        }
    }
}