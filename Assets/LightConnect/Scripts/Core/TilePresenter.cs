using R3;
using LightConnect.Model;
using Color = LightConnect.Model.Color;

namespace LightConnect.Core
{
    public class TilePresenter
    {
        private CompositeDisposable _disposables = new();
        private Tile _model;
        private TileView _view;

        public TilePresenter(Tile model, TileView view)
        {
            _model = model;
            _view = view;

            _view.Initialize();
            _view.Clicked.Subscribe(_ => _model.Rotate(Direction.Right)).AddTo(_disposables);
            _model.Updated.Subscribe(_ => Redraw()).AddTo(_disposables);

            Redraw();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void Redraw()
        {
            _view.SetElement(_model.ElementType);
            _view.SetElementColor(_model.ElementColor, _model.ElementPowered);

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
            {
                bool hasWire = _model.HasWire((Direction)i, out Color color);
                _view.SetWire(hasWire, i, color);
            }
        }

    }
}