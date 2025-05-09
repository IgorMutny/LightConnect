using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Core
{
    public class Wire : Tile
    {
        public Wire(string typeId, Colors color, Vector2Int position, Directions initialDirection, List<Directions> connectors) :
            base(typeId, color, position, initialDirection, connectors)
        { }

        public override void AddPower(Colors color)
        {
            _appliedColors.Add(color);

            if (AllPowersAreOfSameColor())
            {
                _color = _appliedColors[0];
                _powered.Value = true;
            }
            else
            {
                _color = Colors.NONE;
                _powered.Value = false;
            }
        }

        public override void ResetPower()
        {
            _appliedColors.Clear();
            _color = Colors.NONE;
            _powered.Value = false;
        }
    }
}