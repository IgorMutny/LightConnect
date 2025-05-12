using System.Collections.Generic;
using LightConnect.Model;
using R3;

namespace LightConnect.LevelConstruction
{
    public class LevelPresenter
    {
        private CompositeDisposable _disposables = new();
        private Level _model;
        private LevelView _view;
        private List<TilePresenter> _tiles = new();

        public LevelPresenter(Level model, LevelView view)
        {
            _model = model;
            _view = view;

            _view.Initialize();

            foreach (var tile in _model.AllExistingTiles())
            {
                var tileView = _view.AddTile(tile.Position);
                var tilePresenter = new TilePresenter(tile, tileView);
                _tiles.Add(tilePresenter);
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();

            foreach (var tile in _tiles)
                tile.Dispose();
        }
    }
}
