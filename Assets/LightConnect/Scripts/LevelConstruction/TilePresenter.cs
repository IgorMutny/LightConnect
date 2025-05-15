using R3;
using LightConnect.Model;
using UnityEngine;
using Color = LightConnect.Model.Color;
using System;

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
            _view.Clicked.Subscribe(_ => _model.IsSelected = true).AddTo(_disposables);
            _model.UpdatedInternal += Redraw;
            _model.Selected += SetSelection;

            Redraw();
        }

        public void Dispose()
        {
            _model.UpdatedInternal -= Redraw;
            _model.Selected -= SetSelection;
        }

        private void Redraw()
        {
            _view.SetActive(_model.IsActive);
            _view.SetSelected(_model.IsSelected);
            _view.SetElement(_model.ElementType);
            _view.SetElementColor(_model.ElementColor, _model.ElementPowered);

            for (int i = 0; i < Direction.DIRECTIONS_COUNT; i++)
            {
                bool hasWire = _model.HasWire((Direction)i, out Color color);
                _view.SetWire(hasWire, i, color);
            }
        }

        private void SetSelection(Tile tile)
        {
            _view.SetSelected(_model.IsSelected);
        }
    }
}