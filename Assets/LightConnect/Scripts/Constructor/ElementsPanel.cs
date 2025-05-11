using LightConnect.Model;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
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

        private void SelectElement(ElementTypes type)
        {
            LevelPresenter.SetElement(type);
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