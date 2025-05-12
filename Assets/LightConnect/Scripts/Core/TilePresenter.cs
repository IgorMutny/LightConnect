using System;
using UnityEngine;
using R3;
using LightConnect.Model;

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

            /* _view.Clicked.Subscribe(_ => _model.RotateRight());
            _model.Orientation.Subscribe(value => _view.RotateTo(CalculateRotation(value))).AddTo(_disposables);
            _model.Powered.Subscribe(SetColors).AddTo(_disposables); */
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private float CalculateRotation(Sides direction)
        {
            return -(int)direction * 90f;
        }

        private void SetColors(bool powered)
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
            }

            _view.SetColors(wireColor, elementColor); */
        }
    }
}