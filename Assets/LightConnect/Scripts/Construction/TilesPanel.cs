using System;
using LightConnect.Model;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Construction
{
    public class TilesPanel : MonoBehaviour
    {
        [SerializeField] private Button _none;
        [SerializeField] private Button _wire;
        [SerializeField] private Button _battery;
        [SerializeField] private Button _lamp;
        [SerializeField] private Button _warp;

        public event Action<TileTypes> CreateTileRequired;
        public event Action RemoveTileRequired;

        private void Start()
        {
            _none.onClick.AddListener(SelectNone);
            _wire.onClick.AddListener(SelectWire);
            _battery.onClick.AddListener(SelectBattery);
            _lamp.onClick.AddListener(SelectLamp);
            _warp.onClick.AddListener(SelectWarp);
        }

        private void OnDestroy()
        {
            _none.onClick.RemoveListener(SelectNone);
            _wire.onClick.RemoveListener(SelectWire);
            _battery.onClick.RemoveListener(SelectBattery);
            _lamp.onClick.RemoveListener(SelectLamp);
            _warp.onClick.RemoveListener(SelectWarp);
        }

        private void CreateTile(TileTypes type)
        {
            CreateTileRequired?.Invoke(type);
        }

        private void SelectNone()
        {
            RemoveTileRequired?.Invoke();
        }

        private void SelectWire()
        {
            CreateTile(TileTypes.WIRE);
        }

        private void SelectBattery()
        {
            CreateTile(TileTypes.BATTERY);
        }

        private void SelectLamp()
        {
            CreateTile(TileTypes.LAMP);
        }

        private void SelectWarp()
        {
            CreateTile(TileTypes.WARP);
        }
    }
}