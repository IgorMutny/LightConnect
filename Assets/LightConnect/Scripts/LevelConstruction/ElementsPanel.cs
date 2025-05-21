using LightConnect.Model;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.LevelConstruction
{
    public class ElementsPanel : Panel
    {
        [SerializeField] private Button _none;
        [SerializeField] private Button _battery;
        [SerializeField] private Button _lamp;

        protected override void Subscribe()
        {
            _none.onClick.AddListener(SelectNone);
            _battery.onClick.AddListener(SelectBattery);
            _lamp.onClick.AddListener(SelectLamp);
        }

        protected override void Unsubscribe()
        {
            _none.onClick.RemoveListener(SelectNone);
            _battery.onClick.RemoveListener(SelectBattery);
            _lamp.onClick.RemoveListener(SelectLamp);
        }

        private void SelectElement(TileTypes type)
        {
            Constructor.SetTileType(type);
        }

        private void SelectBattery()
        {
            SelectElement(TileTypes.BATTERY);
        }

        private void SelectLamp()
        {
            SelectElement(TileTypes.LAMP);
        }

        private void SelectNone()
        {
            SelectElement(TileTypes.WIRE);
        }
    }
}