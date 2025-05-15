using System;
using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Model
{
    public class Level
    {
        public const int MAX_SIZE = 16;

        private Tile[,] _tiles = new Tile[MAX_SIZE, MAX_SIZE];
        private PowerEvaluator _powerEvaluator;

        public event Action<Tile> TileSelected;

        public Level()
        {
            for (int x = 0; x < MAX_SIZE; x++)
                for (int y = 0; y < MAX_SIZE; y++)
                    CreateTile(x, y);

            _powerEvaluator = new PowerEvaluator(this);
        }

        public Vector2Int CurrentSize { get; private set; }

        public void Dispose()
        {
            foreach (var tile in _tiles)
            {
                tile.Selected -= OnTileSelected;
                tile.EvaluationRequired -= OnEvaluationRequired;
            }
        }

        public IEnumerable<Tile> Tiles()
        {
            foreach (var tile in _tiles)
                if (ContainsTileInCurrentSize(tile))
                    yield return tile;
        }

        public IEnumerable<Tile> AllExistingTiles()
        {
            foreach (var tile in _tiles)
                yield return tile;
        }

        public bool TryGetTile(Vector2Int position, out Tile tile)
        {
            if (position.x < 0 || position.x >= CurrentSize.x || position.y < 0 || position.y >= CurrentSize.y)
            {
                tile = null;
                return false;
            }
            else
            {
                tile = _tiles[position.x, position.y];
                return true;
            }
        }

        public LevelData GetData()
        {
            var data = new LevelData();
            data.SizeX = CurrentSize.x;
            data.SizeY = CurrentSize.y;

            var filledTiles = new List<TileData>();
            foreach (var tile in _tiles)
                if (tile.ElementType != ElementTypes.NONE || tile.WireSetType != WireSetTypes.NONE)
                    filledTiles.Add(tile.GetData());

            data.Tiles = filledTiles.ToArray();

            return data;
        }

        public void SetData(LevelData data)
        {
            foreach (var tileData in data.Tiles)
                _tiles[tileData.PositionX, tileData.PositionY].SetData(tileData);
        }

        public void SetSize(Vector2Int size)
        {
            if (size.x > MAX_SIZE || size.y > MAX_SIZE)
                throw new Exception("New size is too big");

            CurrentSize = size;

            foreach (var tile in _tiles)
                CheckTileState(tile);

            Evaluate();
        }

        public bool ContainsTileInCurrentSize(Tile tile)
        {
            return tile.Position.x < CurrentSize.x && tile.Position.y < CurrentSize.y;
        }

        private void Evaluate()
        {
            if (_powerEvaluator == null)
                return;

            _powerEvaluator.UpdateElements();
            _powerEvaluator.Execute();
            //_powerEvaluator.AllLampsArePowered();
        }

        private void CreateTile(int x, int y)
        {
            var tile = new Tile(new Vector2Int(x, y));
            _tiles[x, y] = tile;
            tile.Selected += OnTileSelected;
            tile.EvaluationRequired += OnEvaluationRequired;
        }

        private void CheckTileState(Tile tile)
        {
            if (tile.Position.x < CurrentSize.x && tile.Position.y < CurrentSize.y)
            {
                tile.IsActive = true;
            }
            else
            {
                tile.IsActive = false;
                tile.IsSelected = false;
            }
        }

        [Obsolete]
        private void OnTileSelected(Tile tile)
        {
            if (tile.IsSelected)
                TileSelected?.Invoke(tile);
        }

        private void OnEvaluationRequired()
        {
            Evaluate();
        }
    }
}