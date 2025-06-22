#if UNITY_EDITOR

using System;
using System.IO;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Construction
{
    public class LevelSaveLoader
    {
        public void Save(Level level, int id)
        {
            string path = Application.streamingAssetsPath + "\\" + id;
            int[] intArray = level.GetData();
            byte[] byteArray = new byte[intArray.Length * 4];

            for (int i = 0; i < intArray.Length; i++)
            {
                byte[] intBytes = BitConverter.GetBytes(intArray[i]);
                Buffer.BlockCopy(intBytes, 0, byteArray, i * 4, 4);
            }

            File.WriteAllBytes(path, byteArray);
        }

        public int[] Load(int id)
        {
            string path = Application.streamingAssetsPath + "\\" + id;

            if (!File.Exists(path))
                throw new Exception($"Level {id} does not exist");

            var byteArray = File.ReadAllBytes(path);
            int[] intArray = new int[byteArray.Length / 4];

            for (int i = 0; i < intArray.Length; i++)
                intArray[i] = BitConverter.ToInt32(byteArray, i * 4);

            return intArray;
        }

        public bool LevelExists(int id)
        {
            string path = Application.streamingAssetsPath + "\\" + id;
            return File.Exists(path);
        }
    }
}

#endif