using System.Collections.Generic;
using LightConnect.Meta;
using LightConnect.Model;
using R3;
using UnityEngine;

namespace LightConnect.Constructor
{
    public class LevelPresenter
    {
        private CompositeDisposable _disposables = new();
        private Level _model;
        private LevelView _view;
        private List<TilePresenter> _tiles = new();
        private TilePresenter _selectedTile;

        public LevelPresenter(Level model, LevelView view)
        {
            _model = model;
            _view = view;

            _view.Initialize();

            /* foreach (var tile in _model.AllExistingTiles())
            {
                var tileView = _view.AddTile(tile.Position);
                var tilePresenter = new TilePresenter(tile, tileView);
                tilePresenter.Clicked.Subscribe(SelectTile).AddTo(_disposables);
                tilePresenter.SetSelected(false);

                tilePresenter.SetElement(tile.ElementType);
                if (tile.ElementType != ElementTypes.NONE)
                    tilePresenter.ResetColors();

                tilePresenter.SetWire(tile.WireType);
                if (tile.WireType != WireTypes.NONE)
                    tilePresenter.SetRotation(tile.Orientation.CurrentValue);

                _tiles.Add(tilePresenter);
            }

            OnCurrentSizeChanged(_model.CurrentSize); */
        }

        /* public Vector2Int CurrentSize => _model.CurrentSize; */

        public void Dispose()
        {
            _disposables.Dispose();

            foreach (var tile in _tiles)
                tile.Dispose();
        }

        public void SelectTile(Vector2Int position)
        {
            foreach (var tile in _tiles)
            {
                if (tile.Position == position)
                {
                    tile.SetSelected(true);
                    _selectedTile = tile;
                }
                else
                {
                    tile.SetSelected(false);
                }
            }
        }

        public void ChangeWidth(int value)
        {
            /* int y = _model.CurrentSize.y;
            var newSize = new Vector2Int(value, y);
            _model.SetCurrentSize(newSize);
            OnCurrentSizeChanged(newSize); */
        }

        public void ChangeHeight(int value)
        {
            /* int x = _model.CurrentSize.x;
            var newSize = new Vector2Int(x, value);
            _model.SetCurrentSize(newSize);
            OnCurrentSizeChanged(newSize); */
        }

        public void SetElement(ElementTypes type)
        {
            if (_selectedTile == null)
                return;

            /* _model.SetElement(_selectedTile.Position, type); */
            _selectedTile.SetElement(type);
        }

        public void SetWire(WireTypes type)
        {
            if (_selectedTile == null)
                return;

            /* _model.SetWire(_selectedTile.Position, type, Sides.UP); */
            _selectedTile.SetWire(type);
        }

        public void SetColor(Colors color)
        {
            if (_selectedTile == null)
                return;

            var tile = _model.GetTile(_selectedTile.Position);

            /* if (tile.ElementType != ElementTypes.NONE)
            {
                var elementType = tile.ElementType;
                _model.SetElement(_selectedTile.Position, elementType, color);
                _selectedTile.ResetColors();
            } */
        }

        public void Rotate(Sides side)
        {
            if (_selectedTile == null)
                return;

            var tile = _model.GetTile(_selectedTile.Position);

            /* if (tile.WireType != WireTypes.NONE)
            {
                var newOrientationSide = Direction.Add(tile.Orientation.CurrentValue, side);
                var wireType = tile.WireType;
                _model.SetWire(_selectedTile.Position, wireType, newOrientationSide);
                _selectedTile.SetRotation(newOrientationSide);
            } */
        }

        private void OnCurrentSizeChanged(Vector2Int newSize)
        {
            foreach (var tile in _tiles)
            {
                bool isActive = tile.Position.x < newSize.x && tile.Position.y < newSize.y;
                tile.SetActive(isActive);

                if (tile == _selectedTile && !isActive)
                    Deselect();
            }
        }

        private void Deselect()
        {
            _selectedTile.SetSelected(false);
            _selectedTile = null;
        }
    }
}
