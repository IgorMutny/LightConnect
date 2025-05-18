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
        private Dictionary<Tile, TilePresenter> _tiles = new();
        private Subject<Tile> _tileSelected = new();

        public LevelPresenter(Level model, LevelView view)
        {
            _model = model;
            _view = view;

            _view.Initialize();

            foreach (var tile in _model.AllExistingTiles())
            {
                var tileView = _view.AddTile(tile.Position);
                var tilePresenter = new TilePresenter(tile, tileView);
                tilePresenter.Selected.Subscribe(_tileSelected.OnNext).AddTo(_disposables);
                _tiles.Add(tile, tilePresenter);
            }
        }

        public Observable<Tile> TileSelected => _tileSelected;

        public void SetSelected(Tile tile, bool isSelected)
        {
            _tiles[tile].SetSelected(isSelected);
        }

        public void Dispose()
        {
            _disposables.Dispose();

            foreach (var tile in _tiles.Values)
                tile.Dispose();
        }
    }
}
