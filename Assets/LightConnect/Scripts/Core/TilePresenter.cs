using LightConnect.Model;
using Color = LightConnect.Model.Color;

namespace LightConnect.Core
{
    public class TilePresenter
    {
        private Tile _model;
        private TileView _view;

        public TilePresenter(Tile model, TileView view)
        {
            _model = model;
            _view = view;
            _view.Initialize();
            _view.Clicked += RotateModel;
            _model.Updated += RedrawView;
            RedrawView();
        }

        public void Dispose()
        {
            _view.Clicked -= RotateModel;
            _model.Updated -= RedrawView;
        }

        private void RedrawView()
        {
            _view.SetElement(_model.ElementType);
            _view.SetElementColor(_model.ElementColor, _model.ElementPowered);

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
            {
                bool hasWire = _model.HasWire((Direction)i, out Color color);
                _view.SetWire(hasWire, i, color);
            }
        }

        private void RotateModel()
        {
            _model.Rotate(Direction.Right);
        }

    }
}