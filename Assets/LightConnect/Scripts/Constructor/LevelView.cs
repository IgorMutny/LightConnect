using LightConnect.Core;
using R3;
using UnityEngine;

namespace LightConnect.Constructor
{
    public class LevelView : MonoBehaviour
    {
        public const int MAX_SIZE = 16;

        private const float TILE_SIZE = 50f;

        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private SizePanel _sizePanel;

        private CompositeDisposable _disposables = new();
        private GameObject[,] _tiles = new GameObject[MAX_SIZE, MAX_SIZE];

        public void Initialize()
        {
            Vector3 initialPosition = new Vector3(
                transform.position.x - MAX_SIZE * TILE_SIZE / 2,
                transform.position.y - MAX_SIZE * TILE_SIZE / 2,
                0f);

            for (int x = 0; x < MAX_SIZE; x++)
            {
                for (int y = 0; y < MAX_SIZE; y++)
                {
                    Vector3 position = new Vector3(initialPosition.x + x * TILE_SIZE, initialPosition.y + y * TILE_SIZE, 0);
                    _tiles[x, y] = Instantiate(_tilePrefab, position, Quaternion.identity, transform);
                }
            }

            _sizePanel.Width.Subscribe(_ => OnSizeChanged()).AddTo(_disposables);
            _sizePanel.Height.Subscribe(_ => OnSizeChanged()).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void OnSizeChanged()
        {
            for (int x = 0; x < MAX_SIZE; x++)
            {
                for (int y = 0; y < MAX_SIZE; y++)
                {
                    bool isActive = x < _sizePanel.Width.CurrentValue && y < _sizePanel.Height.CurrentValue;
                    _tiles[x, y].SetActive(isActive);
                }
            }
        }
    }
}