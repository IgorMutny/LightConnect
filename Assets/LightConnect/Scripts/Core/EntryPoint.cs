using UnityEngine;

namespace LightConnect.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MapSettings _mapSettings;
        [SerializeField] private MapView _mapView;

        private MapPresenter _presenter;

        private void Start()
        {
            var map = _mapSettings.Create();
            _presenter = new MapPresenter(map, _mapView);

            var cameraPosition = Camera.main.transform.position;
            cameraPosition.x = map.Size.x / 2;
            cameraPosition.y = map.Size.y / 2;
            Camera.main.transform.position = cameraPosition;
        }
    }
}