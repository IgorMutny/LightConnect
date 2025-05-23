using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Construction
{
    public class WarpConnectionPanel : Panel
    {
        [SerializeField] private Button _connectionModeButton;

        private Image _buttonImage;

        protected override void Subscribe()
        {
            _connectionModeButton.onClick.AddListener(OnConnectionModeClicked);
            Constructor.ConnectedWarpSelectionModeChanged += OnModeChanged;
            _buttonImage = _connectionModeButton.GetComponent<Image>();
        }

        protected override void Unsubscribe()
        {
            _connectionModeButton.onClick.RemoveListener(OnConnectionModeClicked);
            Constructor.ConnectedWarpSelectionModeChanged -= OnModeChanged;
        }

        private void OnConnectionModeClicked()
        {
            Constructor.ConnectedWarpSelectionMode = !Constructor.ConnectedWarpSelectionMode;
        }

        private void OnModeChanged()
        {
            _buttonImage.color = Constructor.ConnectedWarpSelectionMode ? Color.red : Color.white;
        }
    }
}