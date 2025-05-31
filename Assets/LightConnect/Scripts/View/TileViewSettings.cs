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
        [field: SerializeField] public float ColorChangeSpeed { get; private set; }

        [SerializeField] private List<TileSettings> _tiles;
        [SerializeField] private List<WireSetSettings> _wireSetCenters;
        [SerializeField] private List<ColorSettings> _poweredColors;
        [SerializeField] private List<ColorSettings> _notPoweredColors;
        [SerializeField] private List<TileBasementColorSettings> _poweredBasementColors;
        [SerializeField] private List<TileBasementColorSettings> _notPoweredBasementColors;

        public GameObject Prefab(TileTypes type)
        {
            return _tiles.First(element => element.Type == type).Prefab;
        }

        public Sprite WireSetCenterSprite(WireSetTypes type)
        {
            return _wireSetCenters.First(wire => wire.Type == type).Sprite;
        }

        public UnityEngine.Color Color(Model.Color color, bool isPowered)
        {
            if (isPowered)
                return _poweredColors.First(settings => (int)settings.Name == (int)color).Color;
            else
                return _notPoweredColors.First(settings => (int)settings.Name == (int)color).Color;
        }

        public UnityEngine.Color BasementColor(TileBasementView.Color color, bool isPowered)
        {
            if (isPowered)
                return _poweredBasementColors.First(settings => settings.Name == color).Color;
            else
                return _notPoweredBasementColors.First(settings => settings.Name == color).Color;
        }
    }

    [Serializable]
    public class TileSettings
    {
        [field: SerializeField] public TileTypes Type { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }

    [Serializable]
    public class WireSetSettings
    {
        [field: SerializeField] public WireSetTypes Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }

    [Serializable]
    public class ColorSettings
    {
        [field: SerializeField] public ColorNames Name { get; private set; }
        [field: SerializeField] public UnityEngine.Color Color { get; private set; }
    }

    [Serializable]
    public class TileBasementColorSettings
    {
        [field: SerializeField] public TileBasementView.Color Name { get; private set; }
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