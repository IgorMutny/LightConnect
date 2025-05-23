using LightConnect.Infrastructure;
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
            _view.SetElement(_model.Type);

            if (_model is IColoredTile coloredTile)
                _view.SetElementColor(coloredTile.Color, coloredTile.Powered);

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
            {
                bool hasWire = _model.HasWire((Direction)i, out Color color);
                _view.SetWire(hasWire, i, color);
            }

            if (GameMode.Current == GameMode.Mode.CONSTRUCTOR &&
                _model is WarpTile warpTile)
            {
                var color = warpTile.ConnectedPosition != WarpTile.NONE ? Color.Green : Color.White;
                _view.SetElementColor(color, true);
            }
        }

        private void RotateModel()
        {
            if (GameMode.Current == GameMode.Mode.GAMEPLAY && _model is IRotatableTile)
                _model.Rotate(Direction.Right);
        }

    }
}