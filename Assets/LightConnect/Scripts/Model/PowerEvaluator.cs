using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LightConnect.Model
{
    public class PowerEvaluator
    {
        private Level _level;
        private List<Tile> _batteryTiles = new();
        private List<Tile> _lampTiles = new();
        private List<Tile> _handledTiles = new();
        private List<Tile> _tilesToHandle = new();

        public PowerEvaluator(Level level)
        {
            _level = level;
        }

        public void UpdateElements()
        {
            _batteryTiles.Clear();
            _lampTiles.Clear();

            foreach (var tile in _level.Tiles())
            {
                if (tile.ElementType == ElementTypes.BATTERY)
                    _batteryTiles.Add(tile);

                if (tile.ElementType == ElementTypes.LAMP)
                    _lampTiles.Add(tile);
            }
        }

        public bool AllLampsArePowered()
        {
            if (_lampTiles.Count == 0)
                return false;

            foreach (var lampTile in _lampTiles)
                if (!lampTile.ElementPowered)
                    return false;

            return true;
        }

        public void Execute()
        {
            foreach (var tile in _level.Tiles())
                tile.ResetColors();

            foreach (var batteryTile in _batteryTiles)
                HandleBatteryTile(batteryTile);
        }

        private void HandleBatteryTile(Tile batteryTile)
        {
            HandleTile(batteryTile, batteryTile.ElementColor);

            _handledTiles.Clear();
            _tilesToHandle.Clear();
        }

        private void HandleTile(Tile origin, Color color)
        {
            foreach ((var connectedTile, var direction) in GetConnectedTiles(origin))
            {
                if (!_handledTiles.Contains(connectedTile))
                {
                    origin.HasWire(direction, out Color addedColor);
                    connectedTile.AddColor(-direction, addedColor);
                    _tilesToHandle.Add(connectedTile);
                }
            }

            while (_tilesToHandle.Count > 0)
            {
                var tile = _tilesToHandle.Last();
                _handledTiles.Add(tile);
                _tilesToHandle.Remove(tile);
                HandleTile(tile, color);
            }
        }

        private Dictionary<Tile, Direction> GetConnectedTiles(Tile origin)
        {
            var result = new Dictionary<Tile, Direction>();
            var adjacentTiles = GetAdjacentTiles(origin);

            foreach ((var adjacentTile, var direction) in adjacentTiles)
                if (AreConnected(origin, adjacentTile, direction))
                    result.Add(adjacentTile, direction);

            return result;
        }

        private Dictionary<Tile, Direction> GetAdjacentTiles(Tile origin)
        {
            var result = new Dictionary<Tile, Direction>();

            var position = origin.Position;

            if (_level.TryGetTile(position + Vector2Int.up, out Tile upperTile))
                result.Add(upperTile, Direction.Up);

            if (_level.TryGetTile(position + Vector2Int.down, out Tile lowerTile))
                result.Add(lowerTile, Direction.Down);

            if (_level.TryGetTile(position + Vector2Int.left, out Tile leftTile))
                result.Add(leftTile, Direction.Left);

            if (_level.TryGetTile(position + Vector2Int.right, out Tile rightTile))
                result.Add(rightTile, Direction.Right);

            return result;
        }

        private bool AreConnected(Tile from, Tile to, Direction direction)
        {
            return from.HasWire(direction) && to.HasWire(-direction);
        }
    }
}

