using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace LightConnect.Model
{
    public class Level
    {
        public const int MAX_SIZE = 16;

        private CompositeDisposable _disposables = new();
        private Tile[,] _tiles = new Tile[MAX_SIZE, MAX_SIZE];
        private PowerEvaluator _powerEvaluator;

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
            _disposables.Dispose();
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

            var size = new Vector2Int(data.SizeX, data.SizeY);
            SetSize(size);
        }

        public void SetSize(Vector2Int size)
        {
            if (size.x > MAX_SIZE || size.y > MAX_SIZE)
                throw new Exception("New size is too big");

            CurrentSize = size;

            foreach (var tile in _tiles)
                DefineTileActivity(tile);

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
            tile.EvaluationRequired.Subscribe(_ => Evaluate()).AddTo(_disposables);
        }

        private void DefineTileActivity(Tile tile)
        {
            tile.IsActive = tile.Position.x < CurrentSize.x && tile.Position.y < CurrentSize.y;
        }
    }
}