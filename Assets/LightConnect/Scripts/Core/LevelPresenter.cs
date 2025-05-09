using System.Collections.Generic;
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

            for (int y = 0; y < _model.Size.x; y++)
            {
                for (int x = 0; x < _model.Size.x; x++)
                {
                    var tile = _model.Tiles[x, y];

                    if (tile == null)
                        continue;

                    var tileView = _view.CreateTile(tile.TypeId, new Vector2Int(x, y));
                    var tilePresenter = new TilePresenter(tile, tileView);
                    _presenters.Add(tilePresenter);
                }
            }
        }
    }
}