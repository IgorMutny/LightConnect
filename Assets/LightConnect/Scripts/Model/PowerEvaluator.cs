using System.Collections.Generic;

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
                if (tile.ElementType.CurrentValue == ElementTypes.BATTERY)
                    _batteryTiles.Add(tile);

                if (tile.ElementType.CurrentValue == ElementTypes.LAMP)
                    _lampTiles.Add(tile);
            }
        }

        public bool AllLampsArePowered()
        {
            if (_lampTiles.Count == 0)
                return false;

            foreach (var lampTile in _lampTiles)
                if (!lampTile.Powered.CurrentValue)
                    return false;

            return true;
        }

        public void Execute()
        {
            foreach (var tile in _level.Tiles())
                tile.ResetPower();

            foreach (var batteryTile in _batteryTiles)
                HandleBatteryTile(batteryTile);
        }

        private void HandleBatteryTile(Tile batteryTile)
        {
            HandleTile(batteryTile, batteryTile.ElementColor.CurrentValue);

            _handledTiles.Clear();
            _tilesToHandle.Clear();
        }

        private void HandleTile(Tile origin, Colors color)
        {
            var connectedTiles = GetConnectedTiles(origin);

            if (connectedTiles.Count == 0)
                return;

            foreach (var connectedTile in connectedTiles)
            {
                if (!_handledTiles.Contains(connectedTile))
                {
                    connectedTile.AddColor(color);

                    if (connectedTile.Powered.CurrentValue)
                        _tilesToHandle.Add(connectedTile);
                }
            }

            while (_tilesToHandle.Count > 0)
            {
                var tile = _tilesToHandle[0];
                _handledTiles.Add(tile);
                _tilesToHandle.Remove(tile);
                HandleTile(tile, color);
            }
        }

        private List<Tile> GetConnectedTiles(Tile origin)
        {
            var result = new List<Tile>();
            var adjacentTiles = GetAdjacentTiles(origin);

            foreach (var adjacentTile in adjacentTiles)
                if (AreConnected(origin, adjacentTile))
                    result.Add(adjacentTile);

            return result;
        }

        private List<Tile> GetAdjacentTiles(Tile origin)
        {
            var adjacentTiles = new List<Tile>();

            var position = origin.Position;

            if (position.x > 0)
                adjacentTiles.Add(_level.GetTile(position.x - 1, position.y));

            if (position.x < _level.CurrentSize.CurrentValue.x - 1)
                adjacentTiles.Add(_level.GetTile(position.x + 1, position.y));

            if (position.y > 0)
                adjacentTiles.Add(_level.GetTile(position.x, position.y - 1));

            if (position.y < _level.CurrentSize.CurrentValue.y - 1)
                adjacentTiles.Add(_level.GetTile(position.x, position.y + 1));

            return adjacentTiles;
        }

        private bool AreConnected(Tile from, Tile to)
        {
            bool result = false;
            var direction = GetDirection(from, to);

            if (direction == Sides.UP && from.HasConnectorInDirection(Sides.UP) && to.HasConnectorInDirection(Sides.DOWN))
                result = true;
            else if (direction == Sides.DOWN && from.HasConnectorInDirection(Sides.DOWN) && to.HasConnectorInDirection(Sides.UP))
                result = true;
            else if (direction == Sides.RIGHT && from.HasConnectorInDirection(Sides.RIGHT) && to.HasConnectorInDirection(Sides.LEFT))
                result = true;
            else if (direction == Sides.LEFT && from.HasConnectorInDirection(Sides.LEFT) && to.HasConnectorInDirection(Sides.RIGHT))
                result = true;

            return result;
        }

        private Sides GetDirection(Tile from, Tile to)
        {
            if (from.Position.x == to.Position.x && from.Position.y == to.Position.y - 1)
                return Sides.UP;
            else if (from.Position.x == to.Position.x && from.Position.y == to.Position.y + 1)
                return Sides.DOWN;
            else if (from.Position.x == to.Position.x - 1 && from.Position.y == to.Position.y)
                return Sides.RIGHT;
            else if (from.Position.x == to.Position.x + 1 && from.Position.y == to.Position.y)
                return Sides.LEFT;
            else
                throw new System.Exception($"Tiles {from.Position} & {to.Position} are not adjacent");
        }
    }
}
