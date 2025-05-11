using System.Collections.Generic;

namespace LightConnect.Model
{
    public static class ConnectorHelper
    {
        public static List<Direction> ConnectorsOfWire(WireTypes wireType)
        {
            List<Sides> sides;

            switch (wireType)
            {
                case WireTypes.SINGLE: sides = new List<Sides> { Sides.UP }; break;
                case WireTypes.STRAIGHT: sides = new List<Sides> { Sides.UP, Sides.DOWN }; break;
                case WireTypes.BENT: sides = new List<Sides> { Sides.UP, Sides.RIGHT }; break;
                case WireTypes.TRIPLE: sides = new List<Sides> { Sides.UP, Sides.RIGHT, Sides.DOWN }; break;
                default: sides = new List<Sides> { }; break;
            }

            var result = new List<Direction>();

            foreach (var side in sides)
                result.Add(new Direction(side));

            return result;
        }
    }
}