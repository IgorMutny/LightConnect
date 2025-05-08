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

        public string TypeId => _typeId;
        public Sprite Sprite => _sprite;

        public Tile Create(Vector2Int position)
        {
            return new Tile(_type, _typeId, position, Directions.UP, _connectors);
        }
    }
}