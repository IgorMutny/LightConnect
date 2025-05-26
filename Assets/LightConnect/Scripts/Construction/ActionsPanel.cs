using LightConnect.Model;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Construction
{
    public class ActionsPanel : Panel
    {
        [SerializeField] private Button _rotateRight;
        [SerializeField] private Button _rotateLeft;
        [SerializeField] private Button _connectionModeButton;
        [SerializeField] private Button _wireLocksButton;

        private Image _connectionModeButtonImage;

        protected override void Subscribe()
        {
            _rotateLeft.onClick.AddListener(RotateLeft);
            _rotateRight.onClick.AddListener(RotateRight);
            _connectionModeButton.onClick.AddListener(OnConnectionModeClicked);
            _wireLocksButton.onClick.AddListener(OnWireLocksClicked);

            Constructor.ConnectedWarpSelectionModeChanged += OnModeChanged;
            _connectionModeButtonImage = _connectionModeButton.GetComponent<Image>();
        }

        protected override void Unsubscribe()
        {
            _rotateLeft.onClick.RemoveListener(RotateLeft);
            _rotateRight.onClick.RemoveListener(RotateRight);
            _connectionModeButton.onClick.RemoveListener(OnConnectionModeClicked);
            _wireLocksButton.onClick.RemoveListener(OnWireLocksClicked);

            Constructor.ConnectedWarpSelectionModeChanged -= OnModeChanged;
        }

        private void RotateRight()
        {
            Constructor.Rotate(Direction.Right);
        }

        private void RotateLeft()
        {
            Constructor.Rotate(Direction.Left);
        }

        private void OnConnectionModeClicked()
        {
            Constructor.ConnectedWarpSelectionMode = !Constructor.ConnectedWarpSelectionMode;
        }

        private void OnModeChanged()
        {
            _connectionModeButtonImage.color = Constructor.ConnectedWarpSelectionMode ? UnityEngine.Color.red : UnityEngine.Color.white;
        }

        private void OnWireLocksClicked()
        {
            Constructor.InvertLocked();
        }
    }
}