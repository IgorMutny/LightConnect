using System;
using System.IO;
using Cysharp.Threading.Tasks;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Infrastructure
{
    public class StreamingAssetsLevelLoader : ILevelLoader
    {
        public async UniTask<LevelData> Load(int levelId)
        {
            string path = Application.streamingAssetsPath + "\\" + levelId;

            if (!File.Exists(path))
                throw new Exception($"Level {levelId} does not exist");

            string json = await UniTask.Create(() => UniTask.FromResult(File.ReadAllText(path)));
            LevelData data;

            try
            {
                data = JsonUtility.FromJson<LevelData>(json);
            }
            catch
            {
                throw new Exception($"Level {levelId} can not be read");
            }

            return data;
        }
    }
}