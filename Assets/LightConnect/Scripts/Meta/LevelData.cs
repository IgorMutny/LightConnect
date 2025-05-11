using System;

namespace LightConnect.Meta
{
    [Serializable]
    public class LevelData
    {
        public int SizeX;
        public int SizeY;
        public TileData[] Tiles;
    }
}