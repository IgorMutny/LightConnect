using System.Collections.Generic;
using R3;
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

            _powerEvaluator = new PowerEvaluator(this);
        }

        public Vector2Int CurrentSize => _currentSize;

        public IEnumerable<Tile> Tiles()
        {
            foreach (var tile in _tiles)
                if (ContainsTileInCurrentSize(tile))
                    yield return tile;
        }

        public IEnumerable<Tile> AllExistingTiles()
        {
            foreach (var tile in _tiles)
                yield return tile;
        }

        public Tile GetTile(Vector2Int position)
        {
            return _tiles[position.x, position.y];
        }

        public Tile GetTile(int x, int y)
        {
            return _tiles[x, y];
        }

        public void SetCurrentSize(Vector2Int size)
        {
            if (size.x > MAX_SIZE || size.y > MAX_SIZE)
                throw new System.Exception("New size is too big");

            _currentSize = size;
            Evaluate();
        }

        public void SetWire(Vector2Int position, Wire wire)
        {
            _tiles[position.x, position.y].SetWire(wire);
            Evaluate();
        }

        public void SetWire(Vector2Int position, WireTypes wireType, Sides orientationSide)
        {
            var orientation = new Direction(orientationSide);
            var wire = new Wire(wireType, orientation);
            SetWire(position, wire);
        }

        public void SetElement(Vector2Int position, Element element)
        {
            _tiles[position.x, position.y].SetElement(element);
            Evaluate();
        }

        public void SetElement(Vector2Int position, ElementTypes type)
        {
            Colors color = _tiles[position.x, position.y].ElementColor;
            SetElement(position, type, color);
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

        public void RotateRight(int x, int y)
        {
            _tiles[x, y].RotateRight();
            Evaluate();
        }

        public void RotateLeft(int x, int y)
        {
            _tiles[x, y].RotateLeft();
            Evaluate();
        }

        public bool Evaluate()
        {
            _powerEvaluator.UpdateElements();
            _powerEvaluator.Execute();
            return _powerEvaluator.AllLampsArePowered();
        }

        private bool ContainsTileInCurrentSize(Tile tile)
        {
            return tile.Position.x < _currentSize.x && tile.Position.y < _currentSize.y;
        }
    }
}