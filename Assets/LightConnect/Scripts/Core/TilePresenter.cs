using System;
using UnityEngine;
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
            _model.Powered.Subscribe(SetPowerColor).AddTo(_disposables);
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

        private void SetPowerColor(bool powered)
        {
            Color color = Color.black;

            switch (_model.Color, powered)
            {
                case (Colors.RED, true): color = Color.red; break;
                case (Colors.RED, false): color = new Color(0.5f, 0f, 0f); break;
                case (Colors.GREEN, true): color = Color.green; break;
                case (Colors.GREEN, false): color = new Color(0f, 0.5f, 0f); break;
                case (Colors.BLUE, true): color = Color.blue; break;
                case (Colors.BLUE, false): color = new Color(0f, 0f, 0.5f); break;
                default: color = Color.white; break;
            }

            _view.SetColor(color);
        }
    }
}