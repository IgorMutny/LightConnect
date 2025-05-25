using System;
using LightConnect.Model;
using UnityEngine;
using Color = LightConnect.Model.Color;

namespace LightConnect.Construction
{
    public class Constructor
    {
        private Vector2Int _selectedPosition;
        private Tile _selectedTile;
        private Level _level;
        private LevelSaveLoader _levelSaveLoader;
        private bool _connectedWarpSelectionMode;

        public event Action<Level> LevelCreated;
        public event Action LevelCleared;
        public event Action<Vector2Int> TileSelected;
        public event Action ConnectedWarpSelectionModeChanged;

        public Constructor()
        {
            _levelSaveLoader = new LevelSaveLoader();
        }

        public bool ConnectedWarpSelectionMode
        {
            get
            {
                return _connectedWarpSelectionMode;
            }

            set
            {
                _connectedWarpSelectionMode = value;
                ConnectedWarpSelectionModeChanged?.Invoke();
            }
        }

        public void ClearAndCreateNewLevel()
        {
            Clear();
            CreateNewLevel();
        }

        public void CreateNewLevel()
        {
            _level = new Level();
            LevelCreated?.Invoke(_level);
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
            LevelCreated?.Invoke(_level);
        }

        public void Clear()
        {
            _selectedTile = null;
            _level.Dispose();
            _level = null;
            LevelCleared?.Invoke();
        }

        public bool LevelExists(int id)
        {
            return _levelSaveLoader.LevelExists(id);
        }

        public void SelectTile(Vector2Int position)
        {
            if (ConnectedWarpSelectionMode)
            {
                if (_selectedTile is WarpTile warpTile &&
                     _level.TryGetTile(position, out Tile connected) &&
                      connected is WarpTile connectedWarp)
                {
                    _level.RemoveWarpConnection(warpTile);
                    _level.RemoveWarpConnection(connectedWarp);

                    warpTile.SetConnectedPosition(position);
                    connectedWarp.SetConnectedPosition(_selectedPosition);
                }

                ConnectedWarpSelectionMode = false;
            }
            else
            {
                _selectedPosition = position;
                _level.TryGetTile(position, out Tile tile);
                _selectedTile = tile;
                TileSelected?.Invoke(position);
            }
        }

        public void CreateTile(TileTypes type)
        {
            _level.RemoveTile(_selectedPosition);
            _level.CreateTile(_selectedPosition, type);
            SelectTile(_selectedPosition);
        }

        public void RemoveTile()
        {
            _level.RemoveTile(_selectedPosition);
            _selectedTile = null;
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

            if (_selectedTile is IColoredTile coloredTile)
                coloredTile.SetElementColor(color);
        }

        public void Rotate(Direction side)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.Rotate(side);
        }
    }
}