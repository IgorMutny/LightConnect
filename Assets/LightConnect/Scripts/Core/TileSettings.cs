using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(TileSettings))]

    public class TileSettings : ScriptableObject
    {
        [SerializeField] private Tiles _type;
        [SerializeField] private string _typeId;
        [SerializeField] private List<Directions> _connectors;
        [SerializeField] private Sprite _sprite;

        public string TypeId => _typeId;
        public Sprite Sprite => _sprite;

        public Tile Create()
        {
            return new Tile(_type, _typeId, Directions.UP, _connectors);
        }
    }
}