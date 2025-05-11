using System.Collections.Generic;
using System.Linq;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(AllTilesSettings))]

    public class AllTilesSettings : ScriptableObject
    {
        [SerializeField] private List<ElementSettings> _elements;
        [SerializeField] private List<WireSettings> _wires;

        public ElementSettings ElementSettings(ElementTypes type)
        {
            return _elements.First(element => element.Type == type);
        }

        public WireSettings WireSettings(WireTypes type)
        {
            return _wires.First(wire => wire.Type == type);
        }
    }
}