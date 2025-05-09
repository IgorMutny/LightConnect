using LightConnect.Core;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class ElementsPanel : MonoBehaviour
    {
        [SerializeField] private Button _battery;
        [SerializeField] private Button _lamp;
        [SerializeField] private Button _straightWire;
        [SerializeField] private Button _bentWire;
        [SerializeField] private Button _tripleWire;

        private Subject<string> _elementSelected = new();
        public Observable<string> ElementSelected => _elementSelected;

        public void Initialize()
        {
            _battery.onClick.AddListener(SelectBattery);
            _lamp.onClick.AddListener(SelectLamp);
            _straightWire.onClick.AddListener(SelectStraightWire);
            _bentWire.onClick.AddListener(SelectBentWire);
            _tripleWire.onClick.AddListener(SelectTripleWire);
        }

        public void Dispose()
        {
            _battery.onClick.RemoveListener(SelectBattery);
            _lamp.onClick.RemoveListener(SelectLamp);
            _straightWire.onClick.RemoveListener(SelectStraightWire);
            _bentWire.onClick.RemoveListener(SelectBentWire);
            _tripleWire.onClick.RemoveListener(SelectTripleWire);
        }

        private void SelectElement(string name)
        {
            _elementSelected.OnNext(name);
        }

        private void SelectBattery()
        {
            SelectElement(TileNames.BATTERY);
        }

        private void SelectLamp()
        {
            SelectElement(TileNames.LAMP);
        }

        private void SelectStraightWire()
        {
            SelectElement(TileNames.STRAIGHT_WIRE);
        }

        private void SelectBentWire()
        {
            SelectElement(TileNames.BENT_WIRE);
        }

        private void SelectTripleWire()
        {
            SelectElement(TileNames.TRIPLE_WIRE);
        }
    }
}