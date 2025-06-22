using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LightConnect.Infrastructure
{
    public class StreamingAssetsLevelLoader : ILevelLoader
    {
        public async UniTask<int[]> Load(int id)
        {
            string path = Application.streamingAssetsPath + "\\" + id;

            if (!File.Exists(path))
                throw new Exception($"Level {id} does not exist");

            var byteArray = await UniTask.FromResult(File.ReadAllBytes(path));
            int[] intArray = new int[byteArray.Length / 4];

            for (int i = 0; i < intArray.Length; i++)
                intArray[i] = BitConverter.ToInt32(byteArray, i * 4);

            return intArray;
        }
    }
}