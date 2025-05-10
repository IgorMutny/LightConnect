using System.Collections.Generic;
using R3;
using UnityEngine;

namespace LightConnect.Core
{
    public class Level
    {
        private readonly CompositeDisposable _disposables = new();
        public readonly Vector2Int Size;
        private readonly Tile[,] _tiles;
        private readonly List<Tile> _batteries = new();
        private readonly List<Tile> _lamps = new();

        public Level(Vector2Int size)
        {
            Size = size;
            _tiles = new Tile[size.x, size.y];
        }

        public IEnumerable<Tile> Tiles()
        {
            foreach (var tile in _tiles)
                yield return tile;
        }

        public void SetTile(Tile tile, Vector2Int position)
        {
            _tiles[position.x, position.y] = tile;

            if (tile is Battery)
                _batteries.Add(tile);

            if (tile is Lamp)
                _lamps.Add(tile);
        }

        public void Initialize()
        {
            Randomize();

            foreach (var tile in _tiles)
                tile.Direction.Skip(1).Subscribe(_ => OnTileRotated()).AddTo(_disposables);

            OnTileRotated();
        }

        private void Randomize()
        {
            foreach (var tile in _tiles)
                tile.RotateRandomly();
        }

        private void OnTileRotated()
        {
            foreach (var tile in _tiles)
                tile.ResetPower();

            foreach (var battery in _batteries)
            {
                battery.AddPower(Colors.NONE);
                HandleBattery(battery);
            }
        }

        private void HandleBattery(Tile battery)
        {
            var handledTiles = new List<Tile>();
            var tilesToHandle = new List<Tile>();

            HandleTile(battery, battery.Color, handledTiles, tilesToHandle);
        }

        private void HandleTile(Tile origin, Colors color, List<Tile> handledTiles, List<Tile> tilesToHandle)
        {
            var hasConnectedTiles = HandleAdjacentTiles(origin, color, out List<Tile> connectedTiles);

            if (hasConnectedTiles)
            {
                foreach (var connectedTile in connectedTiles)
                {
                    if (!handledTiles.Contains(connectedTile))
                        tilesToHandle.Add(connectedTile);
                }
            }
            else
            {
                return;
            }

            while (tilesToHandle.Count > 0)
            {
                var tile = tilesToHandle[0];
                handledTiles.Add(tile);
                tilesToHandle.Remove(tile);
                HandleTile(tile, color, handledTiles, tilesToHandle);
            }
        }

        private bool HandleAdjacentTiles(Tile origin, Colors color, out List<Tile> connectedTiles)
        {
            EvaluateAdjacentTiles(origin, out connectedTiles);

            foreach (var tile in connectedTiles)
                tile.AddPower(color);

            return connectedTiles.Count > 0;
        }

        private void EvaluateAdjacentTiles(Tile origin, out List<Tile> connectedTiles)
        {
            connectedTiles = new List<Tile>();

            var adjacentTiles = GetAdjacentTiles(origin);

            foreach (var adjacent in adjacentTiles)
                if (AreConnected(origin, adjacent))
                    connectedTiles.Add(adjacent);
        }

        private List<Tile> GetAdjacentTiles(Tile origin)
        {
            var adjacentTiles = new List<Tile>();

            var position = origin.Position;

            if (position.x > 0)
                adjacentTiles.Add(_tiles[position.x - 1, position.y]);

            if (position.x < Size.x - 1)
                adjacentTiles.Add(_tiles[position.x + 1, position.y]);

            if (position.y > 0)
                adjacentTiles.Add(_tiles[position.x, position.y - 1]);

            if (position.y < Size.y - 1)
                adjacentTiles.Add(_tiles[position.x, position.y + 1]);

            return adjacentTiles;
        }

        private bool AreConnected(Tile from, Tile to)
        {
            bool result = false;
            var direction = GetDirection(from, to);

            if (direction == Directions.UP && from.HasConnectorInDirection(Directions.UP) && to.HasConnectorInDirection(Directions.DOWN))
                result = true;
            else if (direction == Directions.DOWN && from.HasConnectorInDirection(Directions.DOWN) && to.HasConnectorInDirection(Directions.UP))
                result = true;
            else if (direction == Directions.RIGHT && from.HasConnectorInDirection(Directions.RIGHT) && to.HasConnectorInDirection(Directions.LEFT))
                result = true;
            else if (direction == Directions.LEFT && from.HasConnectorInDirection(Directions.LEFT) && to.HasConnectorInDirection(Directions.RIGHT))
                result = true;

            return result;
        }

        private Directions GetDirection(Tile from, Tile to)
        {
            if (from.Position.x == to.Position.x && from.Position.y == to.Position.y - 1)
                return Directions.UP;
            else if (from.Position.x == to.Position.x && from.Position.y == to.Position.y + 1)
                return Directions.DOWN;
            else if (from.Position.x == to.Position.x - 1 && from.Position.y == to.Position.y)
                return Directions.RIGHT;
            else if (from.Position.x == to.Position.x + 1 && from.Position.y == to.Position.y)
                return Directions.LEFT;
            else
                throw new System.Exception($"Tiles {from.Position} & {to.Position} are not adjacent");
        }
    }
}