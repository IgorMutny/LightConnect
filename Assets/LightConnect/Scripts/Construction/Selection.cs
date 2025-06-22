#if UNITY_EDITOR

using UnityEngine;

namespace LightConnect.Construction
{
    public class Selection : MonoBehaviour
    {
        public void SetScale(float scale)
        {
            transform.localScale = new Vector3(scale, scale, 1);
        }

        public void SetPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }
    }
}

#endif