using LightConnect.Model;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.LevelConstruction
{
    public class RotationsPanel : Panel
    {
        [SerializeField] private Button _rotateRight;
        [SerializeField] private Button _rotateLeft;

        protected override void Subscribe()
        {
            _rotateLeft.onClick.AddListener(RotateLeft);
            _rotateRight.onClick.AddListener(RotateRight);
        }

        protected override void Unsubscribe()
        {
            _rotateLeft.onClick.RemoveListener(RotateLeft);
            _rotateRight.onClick.RemoveListener(RotateRight);
        }

        private void RotateRight()
        {
            Constructor.Rotate(Direction.Right);
        }

        private void RotateLeft()
        {
            Constructor.Rotate(Direction.Left);
        }


    }
}