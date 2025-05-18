using System.Collections.Generic;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Core
{
    public class LevelPresenter
    {
        private Level _model;
        private LevelView _view;
        private List<TilePresenter> _presenters = new();

        public LevelPresenter(Level model, LevelView view)
        {
            _model = model;
            _view = view;

            _view.Initialize(_model.CurrentSize);

            foreach (var tile in _model.Tiles())
            {
                if (tile == null)
                    continue;

                var tileView = _view.AddTile(new Vector2Int(tile.Position.x, tile.Position.y));
                var tilePresenter = new TilePresenter(tile, tileView);
                _presenters.Add(tilePresenter);
            }
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
                presenter.Dispose();
        }
    }
}
