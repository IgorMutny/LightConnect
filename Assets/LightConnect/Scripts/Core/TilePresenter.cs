using System;
using R3;

namespace LightConnect.Core
{
    public class TilePresenter : IDisposable
    {
        private CompositeDisposable _disposables = new();
        private Tile _model;
        private TileView _view;

        public TilePresenter(Tile model, TileView view)
        {
            _model = model;
            _view = view;

            _view.Clicked += _model.RotateClockwise;
            _model.Direction.Subscribe(value => _view.RotateTo(CalculateRotation(value))).AddTo(_disposables);
            _model.Powered.Subscribe(_view.SetPower).AddTo(_disposables);
        }

        public void Dispose()
        {
            _view.Clicked -= _model.RotateClockwise;
            _disposables.Dispose();
        }

        private float CalculateRotation(Directions direction)
        {
            return -(int)direction * 90f;
        }
    }
}