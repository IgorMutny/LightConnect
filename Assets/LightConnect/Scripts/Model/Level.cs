using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LightConnect.Model
{
    public class Level
    {
        public const int MAX_SIZE = 16;

        private Dictionary<Vector2Int, Tile> _tiles = new();
        private PowerEvaluator _powerEvaluator;

        public Level()
        {
            _powerEvaluator = new PowerEvaluator(this);
        }

        public Vector2Int CurrentSize { get; private set; }
        public bool IsWon { get; private set; }

        public void Dispose()
        {
            foreach (var tile in _tiles.Values)
                tile.EvaluationRequired -= Evaluate;
        }

        public IEnumerable<Tile> Tiles()
        {
            foreach (var tile in _tiles.Values)
                yield return tile;
        }

        public bool TryGetTile(Vector2Int position, out Tile tile)
        {
            if (_tiles.ContainsKey(position))
            {
                tile = _tiles[position];
                return true;
            }
            else
            {
                tile = null;
                return false;
            }
        }

        public LevelData GetData()
        {
            var data = new LevelData();
            data.SizeX = CurrentSize.x;
            data.SizeY = CurrentSize.y;

            var tiles = new List<TileData>();
            /* foreach (var tile in _tiles)
                tiles.Add(tile.GetData()); */

            data.Tiles = tiles.ToArray();

            return data;
        }

        public void SetData(LevelData data)
        {
            /* foreach (var tileData in data.Tiles)
                _tiles[tileData.PositionX, tileData.PositionY].SetData(tileData);
 */
            var size = new Vector2Int(data.SizeX, data.SizeY);
            /* SetSize(size); */
        }

        private void Evaluate()
        {
            if (_powerEvaluator == null)
                return;

            _powerEvaluator.Execute();

            if (_powerEvaluator.AllLampsArePowered())
                IsWon = true;
        }

        private void AddTile(Vector2Int position, TileTypes type)
        {
            if (_tiles.ContainsKey(position))
                return;

            Tile tile;

            switch (type)
            {
                case TileTypes.WIRE: tile = new WireTile(position); break;
                case TileTypes.BATTERY: tile = new BatteryTile(position); break;
                case TileTypes.LAMP: tile = new LampTile(position); break;
                //case TileTypes.WARP: tile = new WarpTile(position); break;
                default: throw new Exception("Unknown tile type");
            }

            _tiles.Add(position, tile);
            tile.EvaluationRequired += Evaluate;
            _powerEvaluator.UpdateElements();
        }

        private void RemoveTile(Vector2Int position)
        {
            if (!_tiles.ContainsKey(position))
                return;

            Tile tile = _tiles[position];
            tile.EvaluationRequired -= Evaluate;
            _tiles.Remove(position);
            _powerEvaluator.UpdateElements();
        }
    }
}