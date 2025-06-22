#if UNITY_EDITOR

using UnityEngine;

namespace LightConnect.Construction
{
    public class UIView : MonoBehaviour
    {
        [field: SerializeField] public MainPanel MainPanel { get; private set; }
        [field: SerializeField] public TilesPanel TilesPanel { get; private set; }
        [field: SerializeField] public WiresPanel WiresPanel { get; private set; }
        [field: SerializeField] public ColorsPanel ColorsPanel { get; private set; }
        [field: SerializeField] public ActionsPanel ActionsPanel { get; private set; }
    }
}

#endif