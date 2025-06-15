using System.Collections.Generic;
using System.Linq;
using LightConnect.Infrastructure;
using LightConnect.Model;
using UnityEngine;
using UnityEngine.Rendering;

namespace LightConnect.View
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

            if (GameMode.Current == GameMode.Mode.GAMEPLAY)
                CalculateLevelSize();

            _view.Initialize();

            foreach (var tile in _model.Tiles())
                OnTileCreated(tile);

            _model.Win += OnWin;
            _model.TileCreated += OnTileCreated;
            _model.TileRemoved += OnTileRemoved;

            _model.Evaluate();
        }

        public void AllowSounds()
        {
            foreach (var e in _presenters)
                e.Value.SoundsAllowed = true;
        }

        public void Dispose()
        {
            _model.Win -= OnWin;
            _model.TileCreated -= OnTileCreated;
            _model.TileRemoved -= OnTileRemoved;

            foreach (var presenter in _presenters.Values)
                presenter.Dispose();

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
            tilePresenter.RotationByClickAllowed = GameMode.Current == GameMode.Mode.GAMEPLAY;
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
            if (GameMode.Current == GameMode.Mode.GAMEPLAY)
                foreach (var presenter in _presenters.Values)
                    presenter.RotationByClickAllowed = false;
        }
    }
}
