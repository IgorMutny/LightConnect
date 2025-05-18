using LightConnect.Model;
using R3;
using UnityEngine;
using Color = LightConnect.Model.Color;

namespace LightConnect.LevelConstruction
{
    public class Constructor
    {
        private CompositeDisposable _disposables;
        private Tile _selectedTile;
        private Level _level;
        private LevelPresenter _levelPresenter;
        private LevelView _levelView;
        private LevelSaveLoader _levelSaveLoader;
        private Subject<Vector2Int> _newLevelSizeLoaded = new();

        public Constructor(LevelView levelView)
        {
            _levelView = levelView;
            _levelSaveLoader = new LevelSaveLoader();

            CreateNewLevel();
        }

        public Observable<Vector2Int> NewLevelSizeLoaded => _newLevelSizeLoaded;
        public Vector2Int CurrentSize => _level.CurrentSize;

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void CreateNewLevel()
        {
            Clear();

            _disposables = new();
            var size = new Vector2Int(Level.MAX_SIZE / 2, Level.MAX_SIZE / 2);
            _level = new Level();
            _level.SetSize(size);
            _newLevelSizeLoaded.OnNext(_level.CurrentSize);
            _levelPresenter = new LevelPresenter(_level, _levelView);
            _levelPresenter.TileSelected.Subscribe(OnTileSelected).AddTo(_disposables);
        }

        public void Save(int levelNumber)
        {
            _levelSaveLoader.Save(_level, levelNumber);
        }

        public void Load(int levelNumber)
        {
            Clear();

            _disposables = new();
            var levelData = _levelSaveLoader.Load(levelNumber);
            _level = new Level();
            _level.SetData(levelData);
            _newLevelSizeLoaded.OnNext(_level.CurrentSize);
            _levelPresenter = new LevelPresenter(_level, _levelView);
            _levelPresenter.TileSelected.Subscribe(OnTileSelected).AddTo(_disposables);
        }

        public void Clear()
        {
            _selectedTile = null;
            _disposables?.Dispose();
            _levelView?.Clear();
            _levelPresenter?.Dispose();
            _levelPresenter = null;
            _level?.Dispose();
            _level = null;
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