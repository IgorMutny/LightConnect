using LightConnect.Infrastructure;
using LightConnect.Model;
using Color = LightConnect.Model.Color;

namespace LightConnect.View
{
    public class TilePresenter
    {
        private Tile _model;
        private TileView _view;

        public TilePresenter(Tile model, TileView view)
        {
            _model = model;
            _view = view;
            _view.Clicked += RotateModel;
            _model.RedrawingRequired += RedrawView;
            RedrawView();
        }

        public bool RotationByClickAllowed { get; set; }

        public void Dispose()
        {
            _view.Clicked -= RotateModel;
            _model.RedrawingRequired -= RedrawView;
        }

        private void RedrawView()
        {
            _view.CancelColor();

            DefineBasementColor();

            if (_model is IColoredTile coloredTile)
                _view.SetElementColor(coloredTile.Color, coloredTile.ElementPowered, _model.PoweringOrder);

            _view.SetWireSet(_model.WireSetType);
            _view.SetOrientation(_model.Orientation);
            _view.SetLocked(_model.Locked);

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
                DefineWireColor(i);

            RedrawWarpInConstructorMode();
        }

        private void DefineBasementColor()
        {
            TileBasementView.Color basementColor;

            if (_model is WarpTile || (_model is WireTile wireTile && wireTile.Locked))
                basementColor = TileBasementView.Color.GRAY;
            else if ((_model.Position.x + _model.Position.y) % 2 == 0)
                basementColor = TileBasementView.Color.EVEN;
            else
                basementColor = TileBasementView.Color.ODD;

            _view.SetBasementColor(basementColor, _model.WiresPowered, _model.PoweringOrder);
        }

        private void DefineWireColor(int i)
        {
            bool hasWire = _model.HasWire((Direction)i, out Color color);

            if (hasWire)
                if (color != Color.None)
                    _view.SetWireColor((Direction)i, color, _model.PoweringOrder);
                else
                    _view.SetWireColor((Direction)i, Color.None, 0);
        }

        private void RedrawWarpInConstructorMode()
        {
            if (GameMode.Current != GameMode.Mode.CONSTRUCTOR || _model is not WarpTile warpTile)
                return;

            var color = warpTile.ConnectedPosition.HasValue ? TileBasementView.Color.WARP_CONNECTED : TileBasementView.Color.GRAY;
            _view.SetBasementColor(color, _model.WiresPowered, _model.PoweringOrder);
        }

        private void RotateModel()
        {
            if (RotationByClickAllowed && _model is IRotatableTile && !_model.Locked)
                _model.Rotate(Direction.Right);
        }
    }
}