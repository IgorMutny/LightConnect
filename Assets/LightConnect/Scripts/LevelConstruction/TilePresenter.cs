using R3;
using LightConnect.Model;

namespace LightConnect.LevelConstruction
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
            _view.Clicked.Subscribe(_ => _model.IsSelected.Value = true).AddTo(_disposables);
            _model.IsActive.Subscribe(_view.SetActive).AddTo(_disposables);
            _model.IsSelected.Subscribe(_view.SetSelected).AddTo(_disposables);
            _model.WireType.Subscribe(_view.SetWire).AddTo(_disposables);
            _model.Orientation.Subscribe(orientation => _view.SetRotation(orientation.Side)).AddTo(_disposables);
            _model.ElementType.Subscribe(_view.SetElement).AddTo(_disposables);
            _model.ElementColor.Subscribe(_ => ResetColors()).AddTo(_disposables);
            _model.WireColor.Subscribe(_ => ResetColors()).AddTo(_disposables);
            _model.Powered.Subscribe(_ => ResetColors()).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void ResetColors()
        {
            _view.SetColors(
                _model.WireColor.CurrentValue,
                _model.ElementColor.CurrentValue,
                _model.Powered.CurrentValue);
        }
    }
}