using UnityEngine;
using R3;
using LightConnect.Model;

namespace LightConnect.Constructor
{
    public class TilePresenter
    {
        private CompositeDisposable _disposables = new();
        private Tile _model;
        private TileView _view;
        private Subject<Vector2Int> _clicked = new();
        public Observable<Vector2Int> Clicked => _clicked;

        public TilePresenter(Tile model, TileView view)
        {
            _model = model;
            _view = view;

            _view.Initialize();
            _view.Clicked.Subscribe(_ => _clicked.OnNext(_model.Position)).AddTo(_disposables);
        }

        public Vector2Int Position => _model.Position;

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void SetActive(bool value)
        {
            _view.SetActive(value);
        }

        public void SetSelected(bool value)
        {
            _view.SetSelected(value);
        }

        public void SetElement(ElementTypes type)
        {
            _view.SetElement(type);
        }

        public void SetWire(WireTypes type)
        {
            _view.SetWire(type);
        }

        public void SetColor(Colors color)
        {
            _view.SetColor(color);
        }

        public void SetRotation(Sides side)
        {
            _view.SetRotation(side);
        }
    }
}