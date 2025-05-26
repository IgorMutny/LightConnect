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
            _view.Initialize();
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
            _view.StopColorCoroutines();
            RedrawElement();
            RedrawWireSetCenter();

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
                RedrawWire(i);

            RedrawWireLocks();
            RedrawWarpInConstructorMode();
        }

        private void RedrawElement()
        {
            _view.SetElement(_model.Type);

            if (_model is IColoredTile coloredTile)
                _view.SetElementColor(coloredTile.Color, coloredTile.ElementPowered, _model.PoweringOrder);
        }

        private void RedrawWireSetCenter()
        {
            _view.SetWireSet(_model.WireSetType, _model.Orientation);
            if (_model.WiresPowered)
                _view.SetWireSetCenterColor(_model.BlendedColor, _model.PoweringOrder);
            else
                _view.SetWireSetCenterColor(Color.None, 0);
        }

        private void RedrawWire(int i)
        {
            bool hasWire = _model.HasWire((Direction)i, out Color color);
            _view.SetWire(hasWire, i);

            if (!hasWire)
                return;

            if (color != Color.None)
                _view.SetWireColor(i, color, _model.PoweringOrder);
            else
                _view.SetWireColor(i, Color.None, 0);
        }

        private void RedrawWireLocks()
        {
            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
                if (_model.HasWire((Direction)i))
                    _view.SetWireLocks(i, _model.Locked);
                else
                    _view.SetWireLocks(i, false);
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