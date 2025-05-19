using System;
using LightConnect.Model;
using Color = LightConnect.Model.Color;

namespace LightConnect.LevelConstruction
{
    public class TilePresenter
    {
        private Tile _model;
        private TileView _view;

        public event Action<Tile> Selected;

        public TilePresenter(Tile model, TileView view)
        {
            _model = model;
            _view = view;
            _view.Initialize();
            _view.Clicked += OnTileSelected;
            _model.Updated += RedrawView;
            RedrawView();
        }


        public void Dispose()
        {
            _view.Clicked -= OnTileSelected;
            _model.Updated -= RedrawView;
        }

        public void SetSelected(bool isSelected)
        {
            _view.SetSelected(isSelected);
        }

        private void RedrawView()
        {
            _view.SetActive(_model.IsActive);
            _view.SetElement(_model.ElementType);
            _view.SetElementColor(_model.ElementColor, _model.ElementPowered);

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
            {
                bool hasWire = _model.HasWire((Direction)i, out Color color);
                _view.SetWire(hasWire, i, color);
            }
        }

        private void OnTileSelected()
        {
            Selected?.Invoke(_model);
        }
    }
}