using System;
using LightConnect.Model;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Construction
{
    public class ActionsPanel : MonoBehaviour
    {
        [SerializeField] private Button _rotateRight;
        [SerializeField] private Button _rotateLeft;
        [SerializeField] private Button _connectionModeButton;
        [SerializeField] private Button _wireLocksButton;

        private Image _connectionModeButtonImage;

        public event Action<Direction> RotationRequired;
        public event Action ConnectionModeClicked;
        public event Action InvertLocksRequired;

        private void Start()
        {
            _rotateLeft.onClick.AddListener(RotateLeft);
            _rotateRight.onClick.AddListener(RotateRight);
            _connectionModeButton.onClick.AddListener(OnConnectionModeClicked);
            _wireLocksButton.onClick.AddListener(OnWireLocksClicked);

            _connectionModeButtonImage = _connectionModeButton.GetComponent<Image>();
        }

        private void OnDestroy()
        {
            _rotateLeft.onClick.RemoveListener(RotateLeft);
            _rotateRight.onClick.RemoveListener(RotateRight);
            _connectionModeButton.onClick.RemoveListener(OnConnectionModeClicked);
            _wireLocksButton.onClick.RemoveListener(OnWireLocksClicked);
        }

        public void ChangeConnectedWardSelectionMode(bool isConnectedWarpSelectionMode)
        {
            _connectionModeButtonImage.color = isConnectedWarpSelectionMode ? UnityEngine.Color.red : UnityEngine.Color.white;
        }

        private void RotateRight()
        {
            RotationRequired?.Invoke(Direction.Right);
        }

        private void RotateLeft()
        {
            RotationRequired?.Invoke(Direction.Left);
        }

        private void OnConnectionModeClicked()
        {
            ConnectionModeClicked?.Invoke();
        }

        private void OnWireLocksClicked()
        {
            InvertLocksRequired?.Invoke();
        }
    }
}