using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Model
{
    public static class LevelRandomizer
    {
        public static void Randomize(IEnumerable<Tile> tiles)
        {
            int tilesAmount = 0;
            int notRotatedTilesAmount = 0;

            foreach (var tile in tiles)
            {
                if (tile is IRotatableTile && tile.WireSetType != WireSetTypes.NONE)
                {
                    tilesAmount += 1;

                    int rotationsAmount = Random.Range(0, Direction.DIRECTIONS_COUNT);
                    if (rotationsAmount == 0)
                    {
                        notRotatedTilesAmount += 1;
                    }
                    else
                    {
                        for (int i = 0; i < rotationsAmount; i++)
                            tile.Rotate(Direction.Right);
                    }
                }
            }

            if (tilesAmount > 2 && notRotatedTilesAmount > 0 && tilesAmount / notRotatedTilesAmount < 2)
                Randomize(tiles);
        }
    }
}