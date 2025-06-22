using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Model
{
    public class HintHandler
    {
        private const int MAX_CORRECTIONS_PER_HINT = 3;

        private Dictionary<Vector2Int, Direction> _correctWireTileOrientations = new();
        private Level _level;

        public HintHandler(Level level)
        {
            _level = level;

            foreach (var tile in _level.Tiles())
            {
                if (tile is WireTile)
                {
                    _correctWireTileOrientations.Add(tile.Position, tile.Orientation);
                }
            }
        }

        public bool HasWrongOrientatedTiles()
        {
            List<Tile> wrongOrientatedTiles = FindWrongOrientatedTiles();
            return wrongOrientatedTiles.Count > 0;
        }

        public void Help()
        {
            List<Tile> wrongOrientatedTiles = FindWrongOrientatedTiles();

            for (int i = 0; i < MAX_CORRECTIONS_PER_HINT; i++)
                if (wrongOrientatedTiles.Count > 0)
                    CorrectRandomOrientation(wrongOrientatedTiles);
        }

        private List<Tile> FindWrongOrientatedTiles()
        {
            List<Tile> wrongOrientatedTiles = new();

            foreach ((var position, var orientation) in _correctWireTileOrientations)
            {
                _level.TryGetTile(position, out Tile tile);

                if (tile != null && tile.Orientation != orientation)
                    wrongOrientatedTiles.Add(tile);
            }

            return wrongOrientatedTiles;
        }

        private void CorrectRandomOrientation(List<Tile> wrongOrientatedTiles)
        {
            int index = Random.Range(0, wrongOrientatedTiles.Count);
            var tile = (WireTile)wrongOrientatedTiles[index];
            var correctDirection = _correctWireTileOrientations[tile.Position];
            tile.SetOrientation(correctDirection);
            tile.SetLocked(true);
            wrongOrientatedTiles.Remove(tile);
        }
    }
}
