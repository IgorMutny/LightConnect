using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(MapSettings))]

    public class MapSettings : ScriptableObject
    {
        [SerializeField] private Vector2Int _size;
        [SerializeField] private List<TileSettings> _tiles;

        public Map Create()
        {
            var map = new Map(_size);

            int x = 0;
            int y = 0;

            foreach (var tile in _tiles)
            {
                var position = new Vector2Int(x, y);
                map.SetTile(tile.Create(position), position);

                x += 1;
                if (x == _size.x)
                {
                    x = 0;
                    y += 1;
                }
            }

            return map;
        }
    }
}