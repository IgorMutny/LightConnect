using System;
using UnityEngine;

namespace LightConnect.Core
{
    public class TilePresenter : IDisposable
    {
        private Tile _model;
        private TileView _view;

        public TilePresenter(Tile model, TileView view)
        {
            _model = model;
            _view = view;

            _view.Clicked += _model.Rotate;
            _model.Rotated += RotateView;
        }

        public void Dispose()
        {
            _view.Clicked -= _model.Rotate;
            _model.Rotated -= RotateView;
        }

        private void RotateView(int degree)
        {
            var rotation = Quaternion.Euler(0, 0, -degree * 90f);
            _view.RotateTo(rotation);
        }
    }
}