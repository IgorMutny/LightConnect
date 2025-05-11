using System;
using LightConnect.Core;
using LightConnect.Model;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class RotationsPanel : MonoBehaviour
    {
        [SerializeField] private Button _rotateRight;
        [SerializeField] private Button _rotateLeft;

        private Subject<Sides> _rotationRequested = new();
        public Observable<Sides> RotationRequested => _rotationRequested;

        public void Initialize()
        {
            _rotateLeft.onClick.AddListener(RotateLeft);
            _rotateRight.onClick.AddListener(RotateRight);
        }

        public void Dispose()
        {
            _rotateLeft.onClick.RemoveListener(RotateLeft);
            _rotateRight.onClick.RemoveListener(RotateRight);
        }

        private void RotateRight()
        {
            _rotationRequested.OnNext(Sides.RIGHT);
        }

        private void RotateLeft()
        {
            _rotationRequested.OnNext(Sides.LEFT);
        }
    }
}