using LightConnect.Infrastructure;
using LightConnect.Model;
using Color = LightConnect.Model.Color;

namespace LightConnect.Core
{
    public class TilePresenter
    {
        private Tile _model;
        private TileView _view;
        private bool _canBeRotatedByClick;

        public TilePresenter(Tile model, TileView view)
        {
            _model = model;
            _view = view;
            _view.Clicked += RotateModel;
            _model.RedrawingRequired += RedrawView;
            DefineRotatability();
            RedrawView();
        }

        public void Dispose()
        {
            _view.Clicked -= RotateModel;
            _model.RedrawingRequired -= RedrawView;
        }

        private void RedrawView()
        {
            _view.CancelColor();

            if (_model is IColoredTile coloredTile)
                _view.SetElementColor(coloredTile.Color, coloredTile.ElementPowered, _model.PoweringOrder);

            _view.SetWireSet(_model.WireSetType);
            _view.SetOrientation(_model.Orientation);
            _view.SetLocked(_model.Locked);

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
            {
                bool hasWire = _model.HasWire((Direction)i, out Color color);

                if (hasWire)
                    if (color != Color.None)
                        _view.SetWireColor((Direction)i, color, _model.PoweringOrder);
                    else
                        _view.SetWireColor((Direction)i, Color.None, 0);
            }

            RedrawWarpInConstructorMode();
        }

        private void RedrawWarpInConstructorMode()
        {
            if (GameMode.Current != GameMode.Mode.CONSTRUCTOR || _model is not WarpTile warpTile)
                return;

            var color = warpTile.ConnectedPosition != WarpTile.NONE ? Color.Green : Color.White;
            _view.SetElementColor(color, true, 0);
        }

        private void DefineRotatability()
        {
            _canBeRotatedByClick = GameMode.Current == GameMode.Mode.GAMEPLAY && _model is IRotatableTile;
        }

        private void RotateModel()
        {
            if (_canBeRotatedByClick && !_model.Locked)
                _model.Rotate(Direction.Right);
        }
    }
}