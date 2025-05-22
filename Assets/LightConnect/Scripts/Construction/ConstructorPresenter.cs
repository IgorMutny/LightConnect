using LightConnect.Core;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Construction
{
    public class ConstructorPresenter
    {
        private Constructor _model;
        private ConstructorView _view;
        private LevelView _levelView;
        private LevelPresenter _levelPresenter;

        public ConstructorPresenter(Constructor model, ConstructorView view, LevelView levelView)
        {
            _model = model;
            _view = view;
            _levelView = levelView;
            _view.Initialize();
            _levelView.InitialPosition = Vector2Int.zero;
            _levelView.Size = _view.Size;

            _view.TilePlaceholderClicked += OnTilePlaceholderClicked;
            _model.TileSelected += OnTileSelected;
            _model.LevelCreated += OnLevelCreated;
            _model.LevelCleared += OnLevelCleared;
        }

        public void Dispose()
        {
            _view.TilePlaceholderClicked -= OnTilePlaceholderClicked;
            _model.TileSelected -= OnTileSelected;
            _model.LevelCreated -= OnLevelCreated;
            _model.LevelCleared -= OnLevelCleared;
        }

        private void OnTilePlaceholderClicked(Vector2Int position)
        {
            _model.SelectTile(position);
        }

        private void OnTileSelected(Vector2Int position)
        {
            _view.Select(position);
        }

        private void OnLevelCreated(Level level)
        {
            _levelPresenter = new LevelPresenter(level, _levelView, shouldCalculateLevelSize: false);
        }

        private void OnLevelCleared()
        {
            _levelPresenter.Dispose();
            _levelPresenter = null;
        }
    }
}