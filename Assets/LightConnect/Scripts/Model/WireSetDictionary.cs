using System;
using System.Collections.Generic;

namespace LightConnect.Model
{
    public static class WireSetDictionary
    {
        public static List<Direction> GetDirections(WireSetTypes wireSetType)
        {
            switch (wireSetType)
            {
                case WireSetTypes.NONE: return new List<Direction>();
                case WireSetTypes.SINGLE: return new List<Direction> { Direction.Up };
                case WireSetTypes.STRAIGHT: return new List<Direction> { Direction.Up, Direction.Down };
                case WireSetTypes.BENT: return new List<Direction> { Direction.Up, Direction.Right };
                case WireSetTypes.TRIPLE: return new List<Direction> { Direction.Up, Direction.Right, Direction.Down };
                default: throw new Exception("Unknown wire set type");
            }
        }
    }
}