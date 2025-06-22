using System.Collections.Generic;
using System.Linq;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.View
{
    public class LevelPresenter
    {
        private Gameplay _gameplay;
        private Level _model;
        private LevelView _view;
        private Dictionary<Tile, TilePresenter> _presenters = new();

        public LevelPresenter(Gameplay gameplay, LevelView view)
        {
            _gameplay = gameplay;
            _view = view;

            _gameplay.LevelCreated += OnLevelCreated;
            _gameplay.LevelFinished += OnLevelFinished;

            _view.SetConfettiActive(false);
        }

        public void Dispose()
        {
            OnLevelFinished();

            _gameplay.LevelCreated -= OnLevelCreated;
            _gameplay.LevelFinished -= OnLevelFinished;
        }

        private void OnLevelCreated(Level model)
        {
            _model = model;

            CalculateLevelSize();

            _view.Initialize();

            foreach (var tile in _model.Tiles())
                OnTileCreated(tile);

            _model.Win += OnWin;
            _model.TileCreated += OnTileCreated;
            _model.TileRemoved += OnTileRemoved;

            _model.Evaluate();
        }

        private void OnLevelFinished()
        {
            _model.Win -= OnWin;
            _model.TileCreated -= OnTileCreated;
            _model.TileRemoved -= OnTileRemoved;

            foreach (var presenter in _presenters.Values)
                presenter.Dispose();

            _view.SetConfettiActive(false);
            _view.Clear();
        }

        private void CalculateLevelSize()
        {
            int minX = _model.Tiles().Min(tile => tile.Position.x);
            int minY = _model.Tiles().Min(tile => tile.Position.y);
            int maxX = _model.Tiles().Max(tile => tile.Position.x);
            int maxY = _model.Tiles().Max(tile => tile.Position.y);
            var size = new Vector2Int(maxX - minX + 1, maxY - minY + 1);
            var initialPosition = new Vector2Int(minX, minY);
            _view.InitialPosition = initialPosition;
            _view.Size = size;
        }

        private void OnTileCreated(Tile tile)
        {
            var tileView = _view.AddTile(tile.Type, new Vector2Int(tile.Position.x, tile.Position.y));
            var tilePresenter = new TilePresenter(tile, tileView);
            tilePresenter.RotationByClickAllowed = true;
            _presenters.Add(tile, tilePresenter);
        }

        private void OnTileRemoved(Tile tile)
        {
            _view.RemoveTile(new Vector2Int(tile.Position.x, tile.Position.y));
            _presenters[tile].Dispose();
            _presenters.Remove(tile);
        }

        private void OnWin()
        {
            foreach (var presenter in _presenters.Values)
                presenter.RotationByClickAllowed = false;

            _view.SetConfettiActive(true);
        }
    }
}
