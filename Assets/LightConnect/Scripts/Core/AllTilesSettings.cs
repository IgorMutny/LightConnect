using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LightConnect.Core
{
    [CreateAssetMenu(menuName = nameof(AllTilesSettings))]

    public class AllTilesSettings : ScriptableObject
    {
        [SerializeField] private List<TileSettings> _tiles;

        public TileSettings GetTile(string typeId)
        {
            return _tiles.First(tile => tile.TypeId == typeId);
        }
    }
}