using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;

namespace LightConnect.Core
{
    public abstract class Tile
    {
        public readonly string TypeId;
        public readonly Vector2Int Position;

        protected readonly ReactiveProperty<bool> _powered = new();
        protected readonly List<Colors> _appliedColors = new();
        protected Colors _color;
        private readonly Direction _direction;
        private readonly List<Direction> _connectors = new();

        public Tile(string typeId, Colors color, Vector2Int position, Directions initialDirection, List<Directions> connectors)
        {
            TypeId = typeId;
            Position = position;
            _color = color;

            _direction = new Direction(initialDirection);

            foreach (var connector in connectors)
                _connectors.Add(new Direction(connector));
        }

        public ReadOnlyReactiveProperty<Directions> Direction => _direction.Value;
        public ReadOnlyReactiveProperty<bool> Powered => _powered;
        public Colors Color => _color;

        public void RotateClockwise()
        {
            foreach (var connector in _connectors)
                connector.RotateRight();

            _direction.RotateRight();
        }

        public void RotateRandomly()
        {
            var rotationsCount = UnityEngine.Random.Range(0, Core.Direction.DIRECTIONS_COUNT);
            for (int i = 0; i < rotationsCount; i++)
                RotateClockwise();
        }

        public bool HasConnectorInDirection(Directions direction)
        {
            return _connectors.Any(connector => connector.Value.CurrentValue == direction);
        }

        public abstract void AddPower(Colors color);

        public abstract void ResetPower();

        protected bool AllPowersAreOfSameColor()
        {
            if (_appliedColors.Count == 0)
                return false;

            Colors firstColor = _appliedColors[0];

            foreach (var color in _appliedColors)
                if (color != firstColor)
                    return false;

            return true;
        }
    }
}