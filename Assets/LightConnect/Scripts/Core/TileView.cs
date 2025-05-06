using System;
using UnityEngine;

namespace LightConnect.Core
{
    public class TileView : MonoBehaviour
    {
        public event Action Clicked;

        public void OnMouseDown()
        {
            Clicked?.Invoke();
        }

        public void RotateTo(Quaternion quaternion)
        {
            transform.rotation = quaternion;
        }
    }
}
