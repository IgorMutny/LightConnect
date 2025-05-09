using UnityEngine;

namespace LightConnect.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LevelSettings _mapSettings;
        [SerializeField] private LevelView _mapView;

        private LevelPresenter _presenter;

        private void Start()
        {
            var map = _mapSettings.Create();
            map.Initialize();

            _presenter = new LevelPresenter(map, _mapView);

            var cameraPosition = Camera.main.transform.position;
            cameraPosition.x = map.Size.x / 2;
            cameraPosition.y = map.Size.y / 2;
            Camera.main.transform.position = cameraPosition;
        }
    }
}