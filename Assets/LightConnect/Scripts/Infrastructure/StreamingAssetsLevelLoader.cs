using System;
using System.IO;
using Cysharp.Threading.Tasks;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Infrastructure
{
    public class StreamingAssetsLevelLoader : ILevelLoader
    {
        public async UniTask<int[]> Load(int levelId)
        {
            string path = Application.streamingAssetsPath + "\\" + levelId;

            if (!File.Exists(path))
                throw new Exception($"Level {levelId} does not exist");

            string json = await UniTask.Create(() => UniTask.FromResult(File.ReadAllText(path)));

            try
            {
                var data = JsonUtility.FromJson<int[]>(json);
                return data;
            }
            catch
            {
                throw new Exception($"Level {levelId} can not be read");
            }
        }
    }
}