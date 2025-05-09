using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Core
{
    public class Lamp : Tile
    {
        public Lamp(string typeId, Colors color, Vector2Int position, Directions initialDirection, List<Directions> connectors) :
            base(typeId, color, position, initialDirection, connectors)
        { }

        public override void AddPower(Colors color)
        {
            _appliedColors.Add(color);

            if (AllPowersAreOfSameColor() && _appliedColors[0] == _color)
                _powered.Value = true;
            else
                _powered.Value = false;
        }

        public override void ResetPower()
        {
            _appliedColors.Clear();
            _powered.Value = false;
        }
    }
}