using System.Collections.Generic;
using LightConnect.Model;
using LightConnect.View;
using UnityEngine;

namespace LightConnect.Construction
{
    public class LevelPresenter
    {
        private LevelView _view;
        private Constructor _constructor;
        private Level _model;
        private Dictionary<Tile, TilePresenter> _presenters = new();

        public LevelPresenter(Constructor constructor, LevelView view)
        {
            _constructor = constructor;
            _view = view;

            _constructor.LevelCreated += OnLevelCreated;
            _constructor.LevelCleared += OnLevelCleared;

            _view.SetConfettiActive(false);
        }

        public void Dispose()
        {
            _constructor.LevelCreated -= OnLevelCreated;
            _constructor.LevelCleared -= OnLevelCleared;
        }

        public void SetDimensionSize(int dimensionSize)
        {
            _view.InitialPosition = Vector2Int.zero;
            _view.Size = new Vector2Int(dimensionSize, dimensionSize);
            _view.Initialize();
        }

        private void OnLevelCreated(Level model)
        {
            _model = model;

            foreach (var tile in _model.Tiles())
                OnTileCreated(tile);

            _model.TileCreated += OnTileCreated;
            _model.TileRemoved += OnTileRemoved;

            _model.Evaluate();
        }

        private void OnLevelCleared()
        {
            _model.TileCreated -= OnTileCreated;
            _model.TileRemoved -= OnTileRemoved;

            foreach (var presenter in _presenters.Values)
                presenter.Dispose();

            _view.Clear();
        }

        private void OnTileCreated(Tile tile)
        {
            var tileView = _view.AddTile(tile.Type, new Vector2Int(tile.Position.x, tile.Position.y));
            var tilePresenter = new TilePresenter(tile, tileView);
            tilePresenter.RotationByClickAllowed = false;
            _presenters.Add(tile, tilePresenter);
        }

        private void OnTileRemoved(Tile tile)
        {
            _view.RemoveTile(new Vector2Int(tile.Position.x, tile.Position.y));
            _presenters[tile].Dispose();
            _presenters.Remove(tile);
        }
    }
}