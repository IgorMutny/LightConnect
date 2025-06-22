#if UNITY_EDITOR

using LightConnect.Model;

namespace LightConnect.Construction
{
    public class UIPresenter
    {
        private const string LEVEL_ALREADY_EXISTS_MESSAGE = "Level with this id already exists!";
        private const string INVALID_LEVEL_ID_MESSAGE = "Invalid id!";

        private Constructor _constructor;
        private UIView _view;

        private int? _currentLevelId;

        public UIPresenter(Constructor constructor, UIView view)
        {
            _constructor = constructor;
            _view = view;

            _view.MainPanel.LevelIdChanged += OnLevelIdChanged;
            _view.MainPanel.SaveRequired += OnSaveRequired;
            _view.MainPanel.LoadRequired += OnLoadRequired;
            _view.MainPanel.ClearRequired += OnClearRequired;
            _view.TilesPanel.CreateTileRequired += OnCreateTileRequired;
            _view.TilesPanel.RemoveTileRequired += OnRemoveTileRequired;
            _view.WiresPanel.SetWireSetTypeRequired += OnSetWireSetTypeRequired;
            _view.ColorsPanel.SetColorRequired += OnSetColorRequired;
            _view.ActionsPanel.RotationRequired += OnRotationRequired;
            _view.ActionsPanel.ConnectionModeClicked += InvertConnectionMode;
            _view.ActionsPanel.InvertLocksRequired += OnInvertLocksRequired;

            _constructor.ConnectedWarpSelectionModeChanged += OnConnectedWarpSelectionModeChanged;

            _currentLevelId = null;
            _constructor.CreateNewLevel();

            OnLevelIdChanged(string.Empty);
        }

        public void Dispose()
        {
            _view.MainPanel.LevelIdChanged -= OnLevelIdChanged;
            _view.MainPanel.SaveRequired -= OnSaveRequired;
            _view.MainPanel.LoadRequired -= OnLoadRequired;
            _view.MainPanel.ClearRequired -= OnClearRequired;
            _view.TilesPanel.CreateTileRequired -= OnCreateTileRequired;
            _view.TilesPanel.RemoveTileRequired -= OnRemoveTileRequired;
            _view.WiresPanel.SetWireSetTypeRequired -= OnSetWireSetTypeRequired;
            _view.ColorsPanel.SetColorRequired -= OnSetColorRequired;
            _view.ActionsPanel.RotationRequired -= OnRotationRequired;
            _view.ActionsPanel.ConnectionModeClicked -= InvertConnectionMode;
            _view.ActionsPanel.InvertLocksRequired -= OnInvertLocksRequired;

            _constructor.ConnectedWarpSelectionModeChanged -= OnConnectedWarpSelectionModeChanged;
        }

        private void OnLevelIdChanged(string stringId)
        {
            bool result = int.TryParse(stringId, out int levelId);

            if (result)
            {
                _currentLevelId = levelId;

                if (_constructor.LevelExists(_currentLevelId.Value))
                    _view.MainPanel.SetWarningText(LEVEL_ALREADY_EXISTS_MESSAGE);
                else
                    _view.MainPanel.SetWarningText(string.Empty);
            }
            else
            {
                _currentLevelId = null;
                _view.MainPanel.SetWarningText(INVALID_LEVEL_ID_MESSAGE);
            }
        }

        private void OnSaveRequired()
        {
            if (_currentLevelId.HasValue)
                _constructor.Save(_currentLevelId.Value);
            else
                _view.MainPanel.SetWarningText(INVALID_LEVEL_ID_MESSAGE);
        }

        private void OnLoadRequired()
        {
            if (_currentLevelId.HasValue)
            {
                _constructor.Clear();
                _constructor.Load(_currentLevelId.Value);
            }
            else
            {
                _view.MainPanel.SetWarningText(INVALID_LEVEL_ID_MESSAGE);
            }
        }

        private void OnClearRequired()
        {
            _constructor.Clear();
            _constructor.CreateNewLevel();
        }

        private void OnCreateTileRequired(TileTypes type)
        {
            _constructor.CreateTile(type);
        }

        private void OnRemoveTileRequired()
        {
            _constructor.RemoveTile();
        }

        private void OnSetWireSetTypeRequired(WireSetTypes type)
        {
            _constructor.SetWireSetType(type);
        }

        private void OnSetColorRequired(Color color)
        {
            _constructor.SetColor(color);
        }

        private void OnRotationRequired(Direction direction)
        {
            _constructor.Rotate(direction);
        }

        private void InvertConnectionMode()
        {
            _constructor.ConnectedWarpSelectionMode = !_constructor.ConnectedWarpSelectionMode;
        }

        private void OnInvertLocksRequired()
        {
            _constructor.InvertLocks();
        }

        private void OnConnectedWarpSelectionModeChanged()
        {
            _view.ActionsPanel.ChangeConnectedWardSelectionMode(_constructor.ConnectedWarpSelectionMode);
        }
    }
}

#endif