using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Model
{
    public class Level
    {
        public const int MAX_SIZE = 16;

        private Tile[,] _tiles = new Tile[MAX_SIZE, MAX_SIZE];
        private PowerEvaluator _powerEvaluator;
        private Vector2Int _currentSize;

        public Level(Vector2Int size)
        {
            _currentSize = size;

            for (int x = 0; x < MAX_SIZE; x++)
                for (int y = 0; y < MAX_SIZE; y++)
                    _tiles[x, y] = new Tile(new Vector2Int(x, y));

            _powerEvaluator = new PowerEvaluator(_tiles);
        }

        public IEnumerable<Tile> Tiles()
        {
            foreach (var tile in _tiles)
                if (ContainsTileInCurrentSize(tile, _currentSize))
                    yield return tile;
        }

        public void SetCurrentSize(Vector2Int newSize)
        {
            if (newSize.x > MAX_SIZE || newSize.y > MAX_SIZE)
                throw new System.Exception("New size is too big");

            _currentSize = newSize;
            Evaluate();
        }

        public void SetWire(Vector2Int position, Wire wire)
        {
            _tiles[position.x, position.y].SetWire(wire);
            Evaluate();
        }

        public void SetWire(Vector2Int position, Sides orientationSide, WireTypes wireType)
        {
            var connectors = ConnectorHelper.ConnectorsOfWire(wireType);
            var orientation = new Direction(orientationSide);
            var wire = new Wire(orientation, connectors);
            SetWire(position, wire);
        }

        public void SetElement(Vector2Int position, Element element)
        {
            _tiles[position.x, position.y].SetElement(element);
            Evaluate();
        }

        public void SetElement(Vector2Int position, ElementTypes type, Colors color)
        {
            Element element;

            switch (type)
            {
                case ElementTypes.BATTERY: element = new Battery(color); break;
                case ElementTypes.LAMP: element = new Lamp(color); break;
                default: element = null; break;
            }

            SetElement(position, element);
        }

        public void RotateRight(Vector2Int position)
        {
            _tiles[position.x, position.y].RotateRight();
            Evaluate();
        }

        public void RotateLeft(Vector2Int position)
        {
            _tiles[position.x, position.y].RotateLeft();
            Evaluate();
        }

        public bool Evaluate()
        {
            _powerEvaluator.Execute(_currentSize);
            return _powerEvaluator.AllLampsArePowered();
        }

        public static bool ContainsTileInCurrentSize(Tile tile, Vector2Int currentSize)
        {
            return tile.Position.x < currentSize.x && tile.Position.y < currentSize.y;
        }
    }
}