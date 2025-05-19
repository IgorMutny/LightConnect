using System;
using LightConnect.Model;
using UnityEngine;
using Color = LightConnect.Model.Color;

namespace LightConnect.LevelConstruction
{
    public class Constructor
    {
        private Tile _selectedTile;
        private Level _level;
        private LevelPresenter _levelPresenter;
        private LevelView _levelView;
        private LevelSaveLoader _levelSaveLoader;

        public event Action<Vector2Int> LevelLoaded;

        public Constructor(LevelView levelView)
        {
            _levelView = levelView;
            _levelSaveLoader = new LevelSaveLoader();

            CreateNewLevel();
        }

        public Vector2Int CurrentSize => _level.CurrentSize;

        public void Dispose()
        {
            _levelPresenter.TileSelected -= OnTileSelected;
        }

        public void ClearAndCreateNewLevel()
        {
            Clear();
            CreateNewLevel();
        }

        private void CreateNewLevel()
        {
            var size = new Vector2Int(Level.MAX_SIZE / 2, Level.MAX_SIZE / 2);
            _level = new Level();
            _level.SetSize(size);
            LevelLoaded?.Invoke(_level.CurrentSize);
            _levelPresenter = new LevelPresenter(_level, _levelView);
            _levelPresenter.TileSelected += OnTileSelected;
        }

        public void Save(int levelId)
        {
            _levelSaveLoader.Save(_level, levelId);
        }

        public void Load(int levelId)
        {
            Clear();

            var levelData = _levelSaveLoader.Load(levelId);
            _level = new Level();
            _level.SetData(levelData);
            LevelLoaded?.Invoke(_level.CurrentSize);
            _levelPresenter = new LevelPresenter(_level, _levelView);
            _levelPresenter.TileSelected += OnTileSelected;
        }

        public void Clear()
        {
            _selectedTile = null;
            _levelView.Clear();
            _levelPresenter.TileSelected -= OnTileSelected;
            _levelPresenter.Dispose();
            _levelPresenter = null;
            _level.Dispose();
            _level = null;
        }

        public bool LevelExists(int id)
        {
            return _levelSaveLoader.LevelExists(id);
        }

        public void ResizeLevel(Vector2Int size)
        {
            _level.SetSize(size);
            OnLevelResized();
        }

        public void SetElement(ElementTypes type)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.SetElementType(type);
        }

        public void SetWire(WireSetTypes type)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.SetWireSetType(type);
        }

        public void SetTileColor(Color color)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.SetElementColor(color);
        }

        public void Rotate(Direction side)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.Rotate(side);
        }

        private void OnTileSelected(Tile tile)
        {
            Deselect();
            _selectedTile = tile;
            _levelPresenter.SetSelected(_selectedTile, true);
        }

        private void OnLevelResized()
        {
            if (_selectedTile == null)
                return;

            if (_selectedTile.Position.x > _level.CurrentSize.x ||
                    _selectedTile.Position.y > _level.CurrentSize.y)
                Deselect();
        }

        private void Deselect()
        {
            if (_selectedTile == null)
                return;

            _levelPresenter.SetSelected(_selectedTile, false);
            _selectedTile = null;
        }
    }
}