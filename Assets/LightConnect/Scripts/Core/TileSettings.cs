using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(TileSettings))]

    public class TileSettings : ScriptableObject
    {
        [SerializeField] private TileTypes _type;
        [SerializeField] private string _typeId;
        [SerializeField] private List<Directions> _connectors;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Colors _color;

        public string TypeId => _typeId;
        public Sprite Sprite => _sprite;

        public Tile Create(Vector2Int position)
        {
            if (_type == TileTypes.WIRE)
                return new Wire(_typeId, _color, position, Directions.UP, _connectors);
            else if (_type == TileTypes.LAMP)
                return new Lamp(_typeId, _color, position, Directions.UP, _connectors);
            else if (_type == TileTypes.BATTERY)
                return new Battery(_typeId, _color, position, Directions.UP, _connectors);
            else throw new System.Exception("Unknown tile type");
        }
    }
}