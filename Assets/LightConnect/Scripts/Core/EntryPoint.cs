using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private List<Vector2Int> _tilePositions;

        private List<TilePresenter> _presenters = new();

        private void Start()
        {
            foreach (var position in _tilePositions)
                CreateTile(position);
        }

        private void OnDestroy()
        {
            foreach (var presenter in _presenters)
                presenter.Dispose();

            _presenters.Clear();
        }

        private void CreateTile(Vector2Int position)
        {
            var tile = new Tile();
            var worldPosition = new Vector3(position.x, position.y, 0);
            var view = Instantiate(_tilePrefab, worldPosition, Quaternion.identity).GetComponent<TileView>();
            var presenter = new TilePresenter(tile, view);
            _presenters.Add(presenter);
        }
    }
}