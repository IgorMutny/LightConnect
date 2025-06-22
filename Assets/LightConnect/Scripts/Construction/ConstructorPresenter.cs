using LightConnect.View;
using LightConnect.Model;
using UnityEngine;
using Color = LightConnect.Model.Color;

namespace LightConnect.Construction
{
    public class ConstructorPresenter
    {
        private const string LEVEL_ALREADY_EXISTS_MESSAGE = "Level with this id already exists!";
        private const string INVALID_LEVEL_ID_MESSAGE = "Invalid id!";

        private Constructor _model;
        private ConstructorView _view;
        private LevelPresenter _levelPresenter;

        private int? _currentLevelId;

        public ConstructorPresenter(Constructor model, ConstructorView view)
        {
            _model = model;
            _view = view;

            _view.Placeholders.Initialize();
            _view.Level.InitialPosition = Vector2Int.zero;
            _view.Level.Size = _view.Placeholders.Size;

            _view.Placeholders.TilePlaceholderClicked += OnTilePlaceholderClicked;
            _view.Panels.MainPanel.LevelIdChanged += OnLevelIdChanged;
            _view.Panels.MainPanel.SaveRequired += OnSaveRequired;
            _view.Panels.MainPanel.LoadRequired += OnLoadRequired;
            _view.Panels.MainPanel.ClearRequired += OnClearRequired;
            _view.Panels.TilesPanel.CreateTileRequired += OnCreateTileRequired;
            _view.Panels.TilesPanel.RemoveTileRequired += OnRemoveTileRequired;
            _view.Panels.WiresPanel.SetWireSetTypeRequired += OnSetWireSetTypeRequired;
            _view.Panels.ColorsPanel.SetColorRequired += OnSetColorRequired;
            _view.Panels.ActionsPanel.RotationRequired += OnRotationRequired;
            _view.Panels.ActionsPanel.ConnectionModeClicked += InvertConnectionMode;
            _view.Panels.ActionsPanel.InvertLocksRequired += OnInvertLocksRequired;

            _model.TileSelected += OnTileSelected;
            _model.LevelCreated += OnLevelCreated;
            _model.LevelCleared += OnLevelCleared;
            _model.ConnectedWarpSelectionModeChanged += OnConnectedWarpSelectionModeChanged;

            _currentLevelId = null;
            _model.CreateNewLevel();
        }

        public void Dispose()
        {
            _view.Placeholders.TilePlaceholderClicked -= OnTilePlaceholderClicked;
            _view.Panels.MainPanel.LevelIdChanged -= OnLevelIdChanged;
            _view.Panels.MainPanel.SaveRequired -= OnSaveRequired;
            _view.Panels.MainPanel.LoadRequired -= OnLoadRequired;
            _view.Panels.MainPanel.ClearRequired -= OnClearRequired;
            _view.Panels.TilesPanel.CreateTileRequired -= OnCreateTileRequired;
            _view.Panels.TilesPanel.RemoveTileRequired -= OnRemoveTileRequired;
            _view.Panels.WiresPanel.SetWireSetTypeRequired -= OnSetWireSetTypeRequired;
            _view.Panels.ColorsPanel.SetColorRequired -= OnSetColorRequired;
            _view.Panels.ActionsPanel.RotationRequired -= OnRotationRequired;
            _view.Panels.ActionsPanel.ConnectionModeClicked -= InvertConnectionMode;
            _view.Panels.ActionsPanel.InvertLocksRequired -= OnInvertLocksRequired;

            _model.TileSelected -= OnTileSelected;
            _model.LevelCreated -= OnLevelCreated;
            _model.LevelCleared -= OnLevelCleared;
            _model.ConnectedWarpSelectionModeChanged -= OnConnectedWarpSelectionModeChanged;
        }

        private void OnTilePlaceholderClicked(Vector2Int position)
        {
            _model.SelectTile(position);
        }

        private void OnTileSelected(Vector2Int position)
        {
            _view.Placeholders.Select(position);
        }

        private void OnLevelCreated(Level level)
        {
           // _levelPresenter = new LevelPresenter(level, _view.Level);
        }

        private void OnLevelCleared()
        {
            _levelPresenter.Dispose();
            _levelPresenter = null;
        }

        private void OnLevelIdChanged(string stringId)
        {
            bool result = int.TryParse(stringId, out int levelId);

            if (result)
            {
                _currentLevelId = levelId;

                if (_model.LevelExists(_currentLevelId.Value))
                    _view.Panels.MainPanel.SetWarningText(LEVEL_ALREADY_EXISTS_MESSAGE);
                else
                    _view.Panels.MainPanel.SetWarningText(string.Empty);
            }
            else
            {
                _currentLevelId = null;
            }
        }

        private void OnSaveRequired()
        {
            if (_currentLevelId.HasValue)
                _model.Save(_currentLevelId.Value);
            else
                _view.Panels.MainPanel.SetWarningText(INVALID_LEVEL_ID_MESSAGE);
        }

        private void OnLoadRequired()
        {
            if (_currentLevelId.HasValue)
            {
                _model.Clear();
                _model.Load(_currentLevelId.Value);
            }
            else
            {
                _view.Panels.MainPanel.SetWarningText(INVALID_LEVEL_ID_MESSAGE);
            }
        }

        private void OnClearRequired()
        {
            _model.Clear();
            _model.CreateNewLevel();
        }

        private void OnCreateTileRequired(TileTypes type)
        {
            _model.CreateTile(type);
        }

        private void OnRemoveTileRequired()
        {
            _model.RemoveTile();
        }

        private void OnSetWireSetTypeRequired(WireSetTypes type)
        {
            _model.SetWireSetType(type);
        }

        private void OnSetColorRequired(Color color)
        {
            _model.SetColor(color);
        }

        private void OnRotationRequired(Direction direction)
        {
            _model.Rotate(direction);
        }

        private void InvertConnectionMode()
        {
            _model.ConnectedWarpSelectionMode = !_model.ConnectedWarpSelectionMode;
        }

        private void OnInvertLocksRequired()
        {
            _model.InvertLocks();
        }

        private void OnConnectedWarpSelectionModeChanged()
        {
            _view.Panels.ActionsPanel.ChangeConnectedWardSelectionMode(_model.ConnectedWarpSelectionMode);
        }
    }
}