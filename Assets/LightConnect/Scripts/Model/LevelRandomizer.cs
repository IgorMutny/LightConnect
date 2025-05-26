using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Model
{
    public static class LevelRandomizer
    {
        private const float MIN_VALID_ROTATED_TILES_RATIO = 0.5f;

        public static void Randomize(IEnumerable<Tile> tiles)
        {
            int tilesAmount = 0;
            int rotatedTilesAmount = 0;

            foreach (var tile in tiles)
            {
                if (tile is IRotatableTile && tile.WireSetType != WireSetTypes.NONE && !tile.Locked)
                {
                    tilesAmount += 1;

                    int rotationsAmount = Random.Range(0, Direction.DIRECTIONS_COUNT);
                    if (rotationsAmount > 0)
                    {
                        rotatedTilesAmount += 1;

                        for (int i = 0; i < rotationsAmount; i++)
                            tile.Rotate(Direction.Right);
                    }
                }
            }

            if (ShouldRerandomize(tilesAmount, rotatedTilesAmount))
                Randomize(tiles);
        }

        private static bool ShouldRerandomize(int tilesAmount, int rotatedTilesAmount)
        {
            float rotatedTilesRatio =
                tilesAmount == 0 ?
                Mathf.Infinity :
                (float)rotatedTilesAmount / tilesAmount;

            return tilesAmount > 1 / MIN_VALID_ROTATED_TILES_RATIO && rotatedTilesRatio <= MIN_VALID_ROTATED_TILES_RATIO;
        }
    }
}