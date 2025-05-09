using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Core
{
    public class Battery : Tile
    {
        public Battery(string typeId, Colors color, Vector2Int position, Directions initialDirection, List<Directions> connectors) :
            base(typeId, color, position, initialDirection, connectors)
        {
            _powered.Value = true;
        }

        public override void AddPower(Colors color) { }

        public override void ResetPower() { }
    }
}