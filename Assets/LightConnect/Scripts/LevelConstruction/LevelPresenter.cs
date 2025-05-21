using System;
using System.Collections.Generic;
using LightConnect.Model;

namespace LightConnect.LevelConstruction
{
    public class LevelPresenter
    {
        private Level _model;
        private LevelView _view;
        private Dictionary<Tile, TilePresenter> _tiles = new();

        public event Action<Tile> TileSelected;

        public LevelPresenter(Level model, LevelView view)
        {
            _model = model;
            _view = view;

            _view.Initialize();

            foreach (var tile in _model.Tiles())
            {
                var tileView = _view.AddTile(tile.Position);
                var tilePresenter = new TilePresenter(tile, tileView);
                tilePresenter.Selected += OnTileSelected;
                _tiles.Add(tile, tilePresenter);
            }
        }

        public void SetSelected(Tile tile, bool isSelected)
        {
            _tiles[tile].SetSelected(isSelected);
        }

        public void Dispose()
        {
            foreach (var tile in _tiles.Values)
            {
                tile.Selected -= OnTileSelected;
                tile.Dispose();
            }
        }

        private void OnTileSelected(Tile tile)
        {
            TileSelected?.Invoke(tile);
        }
    }
}
