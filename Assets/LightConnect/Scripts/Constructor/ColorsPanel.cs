using LightConnect.Core;
using LightConnect.Model;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class ColorsPanel : Panel
    {
        [SerializeField] private Button _red;
        [SerializeField] private Button _green;
        [SerializeField] private Button _blue;

        protected override void Subscribe()
        {
            _red.onClick.AddListener(SelectRed);
            _green.onClick.AddListener(SelectGreen);
            _blue.onClick.AddListener(SelectBlue);
        }

        protected override void Unsubscribe()
        {
            _red.onClick.RemoveListener(SelectRed);
            _green.onClick.RemoveListener(SelectGreen);
            _blue.onClick.RemoveListener(SelectBlue);
        }

        private void SelectColor(Colors color)
        {
            LevelPresenter.SetColor(color);
        }

        private void SelectGreen()
        {
            SelectColor(Colors.GREEN);
        }

        private void SelectBlue()
        {
            SelectColor(Colors.BLUE);
        }

        private void SelectRed()
        {
            SelectColor(Colors.RED);
        }
    }
}