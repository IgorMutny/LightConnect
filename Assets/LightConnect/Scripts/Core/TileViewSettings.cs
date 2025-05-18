using System;
using System.Collections.Generic;
using System.Linq;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(TileViewSettings))]

    public class TileViewSettings : ScriptableObject
    {
        [SerializeField] private List<ElementSettings> _elements;
        [SerializeField] private List<ColorSettings> _poweredColors;
        [SerializeField] private List<ColorSettings> _notPoweredColors;

        public Sprite ElementSprite(ElementTypes type)
        {
            return _elements.First(element => element.Type == type).Sprite;
        }

        public UnityEngine.Color Color(Model.Color color, bool isPowered)
        {
            if (isPowered)
                return _poweredColors.First(settings => (int)settings.Name == (int)color).Color;
            else
                return _notPoweredColors.First(settings => (int)settings.Name == (int)color).Color;
        }
    }

    [Serializable]
    public class ElementSettings
    {
        [field: SerializeField] public ElementTypes Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }

    [Serializable]
    public class ColorSettings
    {
        [field: SerializeField] public ColorNames Name { get; private set; }
        [field: SerializeField] public UnityEngine.Color Color { get; private set; }
    }

    [Serializable]
    public enum ColorNames
    {
        NONE = 0,
        RED = 1,
        YELLOW = 2,
        ORANGE = 3,
        BLUE = 4,
        MAGENTA = 5,
        GREEN = 6,
        WHITE = 7,
    }
}