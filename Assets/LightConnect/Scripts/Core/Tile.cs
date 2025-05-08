using System;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace LightConnect.Core
{
    public class Tile
    {
        public readonly Tiles Type;
        public readonly string TypeId;

        private readonly Direction _direction;
        private readonly List<Direction> _connectors = new();

        public event Action Rotated;

        public Tile(Tiles type, string typeId, Directions initialDirection, List<Directions> connectors)
        {
            Type = type;
            TypeId = typeId;

            _direction = new Direction(initialDirection);

            foreach (var connector in connectors)
                _connectors.Add(new Direction(connector));
        }

        public ReadOnlyReactiveProperty<Directions> Direction => _direction.Value;

        public void RotateClockwise()
        {
            _direction.RotateClockwise();

            foreach (var connector in _connectors)
                connector.RotateClockwise();

            Rotated?.Invoke();
        }

        public bool HasConnectorInDirection(Directions direction)
        {
            return _connectors.Any(connector => connector.Value.CurrentValue == direction);
        }
    }
}