using System.Collections.Generic;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Core
{
    public class LevelPresenter
    {
        private Level _model;
        private LevelView _view;
        private Dictionary<Tile, TilePresenter> _presenters = new();

        public LevelPresenter(Level model, LevelView view)
        {
            _model = model;
            _view = view;
            _view.Initialize();

            foreach (var tile in _model.Tiles())
                OnTileCreated(tile);

            _model.TileCreated += OnTileCreated;
            _model.TileRemoved += OnTileRemoved;
        }

        public void Dispose()
        {
            _model.TileCreated -= OnTileCreated;
            _model.TileRemoved -= OnTileRemoved;

            foreach (var presenter in _presenters.Values)
                presenter.Dispose();
        }

        private void OnTileCreated(Tile tile)
        {
            var tileView = _view.AddTile(new Vector2Int(tile.Position.x, tile.Position.y));
            var tilePresenter = new TilePresenter(tile, tileView);
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
