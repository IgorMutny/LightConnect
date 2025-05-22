using System;
using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Model
{
    public class Level
    {
        public const int MAX_SIZE = 16;

        private Dictionary<Vector2Int, Tile> _tiles = new();
        private PowerEvaluator _powerEvaluator;

        public event Action<Tile> TileCreated;
        public event Action<Tile> TileRemoved;

        public Level()
        {
            _powerEvaluator = new PowerEvaluator(this);
        }

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

        public int[] GetData()
        {
            var tiles = new List<int>();

            foreach (var tile in _tiles.Values)
            {
                var data = (int)tile.GetData();
                tiles.Add(data);
            }

            return tiles.ToArray();
        }

        public void SetData(int[] tiles)
        {
            foreach (var data in tiles)
            {
                var convertedData = (TileData)data;
                var type = convertedData.Type;
                var position = convertedData.Position;
                var tile = CreateTile(position, type);
                tile.SetData(convertedData);
            }
        }

        private void Evaluate()
        {
            if (_powerEvaluator == null)
                return;

            _powerEvaluator.Execute();

            if (_powerEvaluator.AllLampsArePowered())
                IsWon = true;
        }

        public Tile CreateTile(Vector2Int position, TileTypes type)
        {
            if (_tiles.ContainsKey(position))
                throw new Exception($"Tile {position} already exists");

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
            TileCreated?.Invoke(tile);
            return tile;
        }

        public void RemoveTile(Vector2Int position)
        {
            if (!_tiles.ContainsKey(position))
                return;

            var tile = _tiles[position];
            tile.EvaluationRequired -= Evaluate;
            _tiles.Remove(position);
            _powerEvaluator.UpdateElements();
            TileRemoved?.Invoke(tile);
        }
    }
}