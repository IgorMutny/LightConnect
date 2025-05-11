using System;
using LightConnect.Core;
using LightConnect.Model;
using R3;
using Unity.Collections;
using UnityEngine;

namespace LightConnect.Constructor
{
    public class LevelView : MonoBehaviour
    {
        private const float TILE_SIZE = 50f;

        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private SizePanel _sizePanel;
        [SerializeField] private WiresPanel _wiresPanel;
        [SerializeField] private ColorsPanel _colorsPanel;
        [SerializeField] private ElementsPanel _elementsPanel;
        [SerializeField] private RotationsPanel _rotationsPanel;

        private CompositeDisposable _disposables = new();
        private TileView[,] _tiles = new TileView[Level.MAX_SIZE, Level.MAX_SIZE];
        private TileView _selectedTile;

        public void Initialize()
        {
            Vector3 initialPosition = new Vector3(
                transform.position.x - Level.MAX_SIZE * TILE_SIZE / 2,
                transform.position.y - Level.MAX_SIZE * TILE_SIZE / 2,
                0f);

            for (int x = 0; x < Level.MAX_SIZE; x++)
            {
                for (int y = 0; y < Level.MAX_SIZE; y++)
                {
                    Vector3 position = new Vector3(initialPosition.x + x * TILE_SIZE, initialPosition.y + y * TILE_SIZE, 0);
                    _tiles[x, y] = Instantiate(_tilePrefab, position, Quaternion.identity, transform).GetComponent<TileView>();
                    _tiles[x, y].gameObject.name = $"Tile {x}-{y}";

                    _tiles[x, y].Initialize(
                        new Vector2Int(x, y),
                        WireTypes.NONE,
                        Sides.UP,
                        ElementTypes.NONE,
                        Colors.NONE);

                    _tiles[x, y].Clicked.Subscribe(OnTileClicked).AddTo(_disposables);
                }
            }

            DeselectAllTiles();

            _sizePanel.Width.Subscribe(_ => OnSizeChanged()).AddTo(_disposables);
            _sizePanel.Height.Subscribe(_ => OnSizeChanged()).AddTo(_disposables);
            _wiresPanel.WireSelected.Subscribe(OnWireSelected).AddTo(_disposables);
            _colorsPanel.ColorSelected.Subscribe(OnColorSelected).AddTo(_disposables);
            _elementsPanel.ElementSelected.Subscribe(OnElementSelected).AddTo(_disposables);
            _rotationsPanel.RotationRequested.Subscribe(RotateSelectedTile).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void OnSizeChanged()
        {
            for (int x = 0; x < Level.MAX_SIZE; x++)
            {
                for (int y = 0; y < Level.MAX_SIZE; y++)
                {
                    bool isActive = x < _sizePanel.Width.CurrentValue && y < _sizePanel.Height.CurrentValue;
                    _tiles[x, y].SetActive(isActive);
                }
            }

            if (_selectedTile.IsActive)
                DeselectAllTiles();
        }

        private void DeselectAllTiles()
        {
            _selectedTile = null;

            foreach (var tile in _tiles)
                tile.SetSelected(false);
        }

        private void OnTileClicked(Vector2Int position)
        {
            _selectedTile = _tiles[position.x, position.y];

            foreach (var tile in _tiles)
                tile.SetSelected(tile == _selectedTile);
        }

        private void OnElementSelected(ElementTypes type)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.SetElement(type);
        }

        private void OnWireSelected(WireTypes type)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.SetWire(type);
        }

        private void OnColorSelected(Colors color)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.SetColor(color);
        }

        private void RotateSelectedTile(Sides direction)
        {
            if (_selectedTile == null)
                return;

            _selectedTile.Rotate(direction);
        }
    }
}