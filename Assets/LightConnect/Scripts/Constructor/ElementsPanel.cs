using LightConnect.Core;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class ElementsPanel : MonoBehaviour
    {
        [SerializeField] private Button _none;
        [SerializeField] private Button _battery;
        [SerializeField] private Button _lamp;

        private Subject<ElementTypes> _elementSelected = new();
        public Observable<ElementTypes> ElementSelected => _elementSelected;

        public void Initialize()
        {
            _none.onClick.AddListener(SelectNone);
            _battery.onClick.AddListener(SelectBattery);
            _lamp.onClick.AddListener(SelectLamp);
        }

        public void Dispose()
        {
            _none.onClick.RemoveListener(SelectNone);
            _battery.onClick.RemoveListener(SelectBattery);
            _lamp.onClick.RemoveListener(SelectLamp);
        }

        private void SelectElement(ElementTypes type)
        {
            _elementSelected.OnNext(type);
        }

        private void SelectBattery()
        {
            SelectElement(ElementTypes.BATTERY);
        }

        private void SelectLamp()
        {
            SelectElement(ElementTypes.LAMP);
        }

        private void SelectNone()
        {
            SelectElement(ElementTypes.NONE);
        }
    }
}