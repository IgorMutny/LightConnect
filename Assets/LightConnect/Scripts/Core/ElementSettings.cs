using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(ElementSettings))]

    public class ElementSettings : ScriptableObject
    {
        [field: SerializeField] public ElementTypes Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}