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
            _model.ContentChanged += RedrawView;
            _model.Recolorized += RecolorizeView;
            RedrawView();
        }

        public void Dispose()
        {
            _view.Clicked -= RotateModel;
            _model.ContentChanged -= RedrawView;
            _model.Recolorized -= RecolorizeView;
        }

        private void RedrawView()
        {
            _view.StopColorCoroutines();

            if (_model is IColoredTile coloredTile)
                _view.SetElement(_model.Type, coloredTile.Color);
            else
                _view.SetElement(_model.Type);

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
            {
                bool hasWire = _model.HasWire((Direction)i);
                _view.SetWire(hasWire, i);
            }

            if (GameMode.Current == GameMode.Mode.CONSTRUCTOR &&
                _model is WarpTile warpTile)
            {
                var color = warpTile.ConnectedPosition != WarpTile.NONE ? Color.Green : Color.White;
                _view.SetElementColor(color, true, 0);
            }
        }

        private void RecolorizeView()
        {
            _view.StopColorCoroutines();

            if (_model is IColoredTile coloredTile)
                if (coloredTile.Powered)
                    _view.SetElementColor(coloredTile.Color, coloredTile.Powered, _model.OrderInPowerChain);
                else
                    _view.SetElementColor(coloredTile.Color, coloredTile.Powered, 0);

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
            {
                bool hasWire = _model.HasWire((Direction)i, out Color color);

                if (hasWire)
                {
                    if (color != Color.None)
                        _view.SetWireColor(i, color, _model.OrderInPowerChain);
                    else
                        _view.SetWireColor(i, color, 0);
                }
            }

        }

        private void RotateModel()
        {
            if (GameMode.Current == GameMode.Mode.GAMEPLAY && _model is IRotatableTile)
                _model.Rotate(Direction.Right);
        }

    }
}