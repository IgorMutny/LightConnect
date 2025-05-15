using System;
using System.Collections.Generic;
using System.Linq;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(AllTilesSettings))]

    [Obsolete]
    public class AllTilesSettings : ScriptableObject
    {
        [SerializeField] private List<ElementSettings> _elements;

        public ElementSettings ElementSettings(ElementTypes type)
        {
            return _elements.First(element => element.Type == type);
        }
    }
}