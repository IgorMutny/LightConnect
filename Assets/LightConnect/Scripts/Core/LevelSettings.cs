using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(LevelSettings))]

    public class LevelSettings : ScriptableObject
    {
        [SerializeField] private Vector2Int _size;
        [SerializeField] private List<ElementSettings> _tiles;

        public Level Create()
        {
            var map = new Level(_size);

            /* int x = 0;
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
            } */

            return map;
        }
    }
}