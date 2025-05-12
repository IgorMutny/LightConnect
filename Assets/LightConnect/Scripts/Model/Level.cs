using System.Collections.Generic;
using LightConnect.Meta;
using R3;
using UnityEngine;

namespace LightConnect.Model
{
    public class Level
    {
        public const int MAX_SIZE = 16;

        private CompositeDisposable _disposables = new();
        private Tile[,] _tiles = new Tile[MAX_SIZE, MAX_SIZE];
        private ReactiveProperty<Vector2Int> _currentSize = new();
        private PowerEvaluator _powerEvaluator;

        public Level()
        {
            for (int x = 0; x < MAX_SIZE; x++)
                for (int y = 0; y < MAX_SIZE; y++)
                    CreateTile(x, y);

            _powerEvaluator = new PowerEvaluator(this);
        }

        public ReadOnlyReactiveProperty<Vector2Int> CurrentSize => _currentSize;

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

        public Tile GetTile(Vector2Int position)
        {
            return _tiles[position.x, position.y];
        }

        public Tile GetTile(int x, int y)
        {
            return _tiles[x, y];
        }

        public LevelData GetData()
        {
            var data = new LevelData();
            data.SizeX = _currentSize.Value.x;
            data.SizeY = _currentSize.Value.y;

            var filledTiles = new List<TileData>();
            foreach (var tile in _tiles)
                if (tile.ElementType.CurrentValue != ElementTypes.NONE || tile.WireType.CurrentValue != WireTypes.NONE)
                    filledTiles.Add(tile.GetData());

            data.Tiles = filledTiles.ToArray();

            return data;
        }

        public void SetData(LevelData data)
        {
            foreach (var tileData in data.Tiles)
                _tiles[tileData.PositionX, tileData.PositionY].SetData(tileData);
        }

        public void SetCurrentSize(Vector2Int size)
        {
            if (size.x > MAX_SIZE || size.y > MAX_SIZE)
                throw new System.Exception("New size is too big");

            _currentSize.Value = size;
            Evaluate();
        }

        private void Evaluate()
        {
            if (_powerEvaluator == null)
                return;

            _powerEvaluator.UpdateElements();
            _powerEvaluator.Execute();
            //_powerEvaluator.AllLampsArePowered();
        }

        private bool ContainsTileInCurrentSize(Tile tile)
        {
            return tile.Position.x < _currentSize.Value.x && tile.Position.y < _currentSize.Value.y;
        }

        private void CreateTile(int x, int y)
        {
            _tiles[x, y] = new Tile(new Vector2Int(x, y));
            _tiles[x, y].WireType.Subscribe(_ => Evaluate()).AddTo(_disposables);
            _tiles[x, y].Orientation.Subscribe(_ => Evaluate()).AddTo(_disposables);
            _tiles[x, y].ElementType.Subscribe(_ => Evaluate()).AddTo(_disposables);
            _tiles[x, y].ElementColor.Subscribe(_ => Evaluate()).AddTo(_disposables);
        }
    }
}