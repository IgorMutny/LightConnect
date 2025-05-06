using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _tileIPrefab;
        [SerializeField] private GameObject _tileLPrefab;
        [SerializeField] private GameObject _tileTPrefab;
        [SerializeField] private Vector2Int _fieldSize;

        private List<TilePresenter> _presenters = new();

        private void Start()
        {
            for (int x = 0; x < _fieldSize.x; x++)
                for (int y = 0; y < _fieldSize.y; y++)
                    CreateTile(new Vector2Int(x, y));

            var cameraPosition = Camera.main.transform.position;
            cameraPosition.x = _fieldSize.x / 2;
            cameraPosition.y = _fieldSize.y / 2;
            Camera.main.transform.position = cameraPosition;
        }

        private void OnDestroy()
        {
            foreach (var presenter in _presenters)
                presenter.Dispose();

            _presenters.Clear();
        }

        private void CreateTile(Vector2Int position)
        {
            var orientation = Random.Range(0, 4);
            var connectors = new List<int>();

            GameObject prefab = null;

            var connectorsType = Random.Range(0, 3);

            switch (connectorsType)
            {
                case 0: connectors.Add(0); connectors.Add(1); prefab = _tileLPrefab; break;
                case 1: connectors.Add(0); connectors.Add(2); prefab = _tileIPrefab; break;
                case 2: connectors.Add(0); connectors.Add(1); connectors.Add(2); prefab = _tileTPrefab; break;
            }

            var tile = new Tile(orientation, connectors);
            var worldPosition = new Vector3(position.x, position.y, 0);
            var view = Instantiate(prefab, worldPosition, Quaternion.identity).GetComponent<TileView>();
            var presenter = new TilePresenter(tile, view);
            _presenters.Add(presenter);
        }
    }
}