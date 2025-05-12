using System;
using LightConnect.Model;
using R3;
using UnityEngine;

namespace LightConnect.LevelConstruction
{
    public class Constructor
    {
        private IDisposable _disposable;
        private Tile _selectedTile;
        private Level _level;
        private LevelPresenter _levelPresenter;
        private LevelView _levelView;
        private LevelSaveLoader _levelSaveLoader;

        public ReadOnlyReactiveProperty<Vector2Int> CurrentSize => _level.CurrentSize;

        public Constructor(LevelView levelView)
        {
            _levelView = levelView;
            _levelSaveLoader = new LevelSaveLoader();

            CreateNewLevel();
        }

        public void CreateNewLevel()
        {
            Clear();

            var size = new Vector2Int(Level.MAX_SIZE / 2, Level.MAX_SIZE / 2);
            _level = new Level();
            _disposable = _level.TileSelected.Subscribe(OnTileSelected);
            _level.SetSize(size);
            _levelPresenter = new LevelPresenter(_level, _levelView);
        }

        public void Save(int levelNumber)
        {
            _levelSaveLoader.Save(_level, levelNumber);
        }

        public void Load(int levelNumber)
        {
            Clear();

            var levelData = _levelSaveLoader.Load(levelNumber);
            _level = new Level();
            _disposable = _level.TileSelected.Subscribe(OnTileSelected);
            _level.SetData(levelData);
            _levelPresenter = new LevelPresenter(_level, _levelView);
        }

        public void Clear()
        {
            _selectedTile = null;
            _disposable?.Dispose();
            _levelView?.Clear();
            _levelPresenter?.Dispose();
            _levelPresenter = null;
            _level?.Dispose();
            _level = null;
        }

        public void ResizeLevel(Vector2Int size)
        {
            _level.SetSize(size);
        }

        public void SetElement(ElementTypes type)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.SetElementType(type);
        }

        public void SetWire(WireTypes type)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.SetWireType(type);
        }

        public void SetTileColor(Colors color)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.SetColor(color);
        }

        public void Rotate(Sides side)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.Rotate(side);
        }

        private void OnTileSelected(Tile tile)
        {
            if (_selectedTile != null)
                _selectedTile.IsSelected.Value = false;

            _selectedTile = tile;
        }
    }
}