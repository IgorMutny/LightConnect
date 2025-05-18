using R3;
using LightConnect.Model;
using Color = LightConnect.Model.Color;

namespace LightConnect.LevelConstruction
{
    public class TilePresenter
    {
        private CompositeDisposable _disposables = new();
        private Tile _model;
        private TileView _view;
        private Subject<Tile> _selected = new();

        public TilePresenter(Tile model, TileView view)
        {
            _model = model;
            _view = view;

            _view.Initialize();
            _view.Clicked.Subscribe(_ => _selected.OnNext(_model)).AddTo(_disposables);
            _model.Updated.Subscribe(_ => Redraw()).AddTo(_disposables);

            Redraw();
        }

        public Observable<Tile> Selected => _selected;

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void SetSelected(bool isSelected)
        {
            _view.SetSelected(isSelected);
        }

        private void Redraw()
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

    }
}