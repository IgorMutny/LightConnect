using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace LightConnect.Core
{
    public class Tile
    {
        public readonly TileTypes Type;
        public readonly string TypeId;
        public readonly Vector2Int Position;
        public readonly ReactiveProperty<bool> Powered = new();

        private readonly Direction _direction;
        private readonly List<Direction> _connectors = new();

        public Tile(TileTypes type, string typeId, Vector2Int position, Directions initialDirection, List<Directions> connectors)
        {
            Type = type;
            TypeId = typeId;
            Position = position;

            _direction = new Direction(initialDirection);

            foreach (var connector in connectors)
                _connectors.Add(new Direction(connector));
        }

        public ReadOnlyReactiveProperty<Directions> Direction => _direction.Value;

        public void RotateClockwise()
        {
            foreach (var connector in _connectors)
                connector.RotateClockwise();

            _direction.RotateClockwise();
        }

        public bool HasConnectorInDirection(Directions direction)
        {
            return _connectors.Any(connector => connector.Value.CurrentValue == direction);
        }
    }
}