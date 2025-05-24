using System;
using System.Collections.Generic;
using System.Linq;

namespace LightConnect.Model
{
    public class WireSet
    {
        private List<Wire> _wires = new();

        public WireSet()
        {
            Type = WireSetTypes.NONE;
            Orientation = Direction.Up;
        }

        public WireSetTypes Type { get; private set; }
        public Direction Orientation { get; private set; }
        public Color BlendedColor => GetBlendedColor();

        public void SetType(WireSetTypes type)
        {
            Type = type;
            Orientation = Direction.Up;

            _wires.Clear();

            var directions = WireSetDictionary.GetDirections(type);
            foreach (var direction in directions)
                _wires.Add(new Wire(direction));
        }

        public void SetOrientation(Direction newOrientation)
        {
            var difference = newOrientation - Orientation;

            Orientation = newOrientation;

            foreach (var wire in _wires)
                wire.Rotate(difference);
        }

        public void Rotate(Direction side)
        {
            Orientation += side;

            foreach (var wire in _wires)
                wire.Rotate(side);
        }

        public bool HasWire(Direction direction)
        {
            return _wires.Any(wire => wire.Orientation == direction);
        }

        public bool HasWire(Direction direction, out Color color)
        {
            var wire = _wires.FirstOrDefault(wire => wire.Orientation == direction);
            color = wire != null ? wire.Color : Color.None;
            return wire != null;
        }

        public bool HasAnyColorInWires()
        {
            return _wires.Any(wire => wire.Color != Color.None);
        }

        public void AddColor(Direction direction, Color color)
        {
            var wire = _wires.FirstOrDefault(wire => wire.Orientation == direction);
            if (wire != null)
                wire.AddColor(color);
        }

        public void AddColorToAllWires(Color color)
        {
            foreach (var wire in _wires)
                wire.AddColor(color);
        }

        public void ResetColors()
        {
            foreach (var wire in _wires)
                wire.ResetColor();
        }

        private Color GetBlendedColor()
        {
            return _wires.Aggregate(Color.None, (color, wire) => color + wire.Color);
        }
    }
}