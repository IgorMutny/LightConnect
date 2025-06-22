#if UNITY_EDITOR

using UnityEngine;

namespace LightConnect.Construction
{
    public class PlaceholdersPresenter
    {
        private PlaceholdersView _view;
        private Constructor _constructor;

        public PlaceholdersPresenter(Constructor constructor, PlaceholdersView view)
        {
            _constructor = constructor;
            _view = view;

            _view.Initialize();
            _view.TilePlaceholderClicked += OnPlaceholderClicked;

            _constructor.TileSelected += OnTileSelected;
        }

        public void Dispose()
        {
            _view.TilePlaceholderClicked -= OnPlaceholderClicked;
            _constructor.TileSelected -= OnTileSelected;
        }

        private void OnPlaceholderClicked(Vector2Int position)
        {
            _constructor.SelectTile(position);
        }

        private void OnTileSelected(Vector2Int position)
        {
            _view.Select(position);
        }
    }
}

#endif