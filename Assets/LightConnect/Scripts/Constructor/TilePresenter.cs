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

            _model.Powered.Subscribe(SetColors).AddTo(_disposables);
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
            SetColors(_model.Powered.CurrentValue);
        }

        public void SetWire(WireTypes type)
        {
            _view.SetWire(type);
            SetColors(_model.Powered.CurrentValue);
        }

        public void SetRotation(Sides side)
        {
            _view.SetRotation(side);
        }

        public void ResetColors()
        {
            SetColors(_model.Powered.CurrentValue);
        }

        public void SetColors(bool powered)
        {
            Color wireColor;
            Color elementColor;

            /* switch (_model.WireColor)
            {
                case Colors.RED: wireColor = Color.red; break;
                case Colors.GREEN: wireColor = Color.green; break;
                case Colors.BLUE: wireColor = Color.blue; break;
                default: wireColor = Color.white; break;
            }

            switch (_model.ElementColor, powered)
            {
                case (Colors.RED, true): elementColor = Color.red; break;
                case (Colors.RED, false): elementColor = new Color(0.5f, 0f, 0f); break;
                case (Colors.GREEN, true): elementColor = Color.green; break;
                case (Colors.GREEN, false): elementColor = new Color(0f, 0.5f, 0f); break;
                case (Colors.BLUE, true): elementColor = Color.blue; break;
                case (Colors.BLUE, false): elementColor = new Color(0f, 0f, 0.5f); break;
                default: elementColor = Color.white; break;
            } */

            /* _view.SetColors(wireColor, elementColor); */
        }
    }
}