using System;

namespace LightConnect.Model
{
    [Serializable]
    public class LevelData
    {
        public int SizeX;
        public int SizeY;
        public TileData[] Tiles;
    }
}