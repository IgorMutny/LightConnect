using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(WireSettings))]

    public class WireSettings : ScriptableObject
    {
        [field: SerializeField] public WireTypes Type { get; private set; }
        [field: SerializeField] public Sides[] Directions { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}