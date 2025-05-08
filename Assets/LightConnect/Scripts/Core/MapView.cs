using UnityEngine;

namespace LightConnect.Core
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private AllTilesSettings _allTilesSettings;
        [SerializeField] private GameObject _tilePrefab;

        public TileView CreateTile(string typeId, Vector2Int position)
        {
            var worldPosition = new Vector3(position.x, position.y, 0);
            var tileObject = Instantiate(_tilePrefab, worldPosition, Quaternion.identity, transform);
            var tile = tileObject.GetComponent<TileView>();
            var settings = _allTilesSettings.GetTile(typeId);
            var sprite = settings.Sprite;
            tile.SetSprite(sprite);

            return tile;
        }
    }
}