using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LightConnect.Model
{
    public class PowerEvaluator
    {
        private Level _level;
        private List<BatteryTile> _batteryTiles = new();
        private List<LampTile> _lampTiles = new();
        private List<Tile> _poweredTiles = new();

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
                if (tile is BatteryTile batteryTile)
                    _batteryTiles.Add(batteryTile);

                if (tile is LampTile lampTile)
                    _lampTiles.Add(lampTile);
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
            _poweredTiles.Clear();

            foreach (var tile in _level.Tiles())
            {
                if (tile.WiresPowered)
                    _poweredTiles.Add(tile);

                tile.ResetColors();
            }

            foreach (var batteryTile in _batteryTiles)
            {
                var path = new Path() { batteryTile };
                HandlePath(path);
            }
        }

        private void HandlePath(Path path)
        {
            var origin = path.Last();
            var connectedTiles = GetConnectedTiles(origin);

            foreach ((var connectedTile, var direction) in connectedTiles)
            {
                if (!path.Contains(connectedTile))
                {
                    HandleTile(origin, connectedTile, direction, path);

                    if (connectedTile is WarpTile warpTile)
                        HandleWarp(warpTile, path);
                }
            }
        }

        private void HandleTile(Tile from, Tile to, Direction direction, Path path)
        {
            from.HasWire(direction, out Color color);
            if (color != Color.None)
            {
                if (_poweredTiles.Contains(to))
                    path.ResetOrder();

                to.AddColor(-direction, color, path.CurrentOrder);
                var newPath = new Path(path) { to };
                HandlePath(newPath);
            }
        }

        private void HandleWarp(WarpTile warpTile, Path path)
        {
            if (warpTile.ConnectedPosition == WarpTile.NONE)
                return;

            var color = warpTile.BlendedColor;
            _level.TryGetTile(warpTile.ConnectedPosition, out Tile connectedTile);

            var connectedWarp = (WarpTile)connectedTile;

            if (connectedWarp != null)
            {
                connectedWarp.AddColorToAllWires(color);
                var newPath = new Path(path) { connectedWarp };
                HandlePath(newPath);
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

        private class Path : List<Tile>
        {
            public Path() : base()
            {
                CurrentOrder = 0;
            }

            public Path(Path parentPath) : base(parentPath)
            {
                CurrentOrder = parentPath.CurrentOrder + 1;
            }

            public int CurrentOrder { get; private set; }

            public void ResetOrder()
            {
                CurrentOrder = 0;
            }
        }
    }
}

