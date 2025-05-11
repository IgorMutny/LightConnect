using LightConnect.Core;
using LightConnect.Model;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class ColorsPanel : MonoBehaviour
    {
        [SerializeField] private Button _red;
        [SerializeField] private Button _green;
        [SerializeField] private Button _blue;

        private Subject<Colors> _colorSelected = new();
        public Observable<Colors> ColorSelected => _colorSelected;

        public void Initialize()
        {
            _red.onClick.AddListener(SelectRed);
            _green.onClick.AddListener(SelectGreen);
            _blue.onClick.AddListener(SelectBlue);
        }

        public void Dispose()
        {
            _red.onClick.RemoveListener(SelectRed);
            _green.onClick.RemoveListener(SelectGreen);
            _blue.onClick.RemoveListener(SelectBlue);
        }

        private void SelectColor(Colors color)
        {
            _colorSelected.OnNext(color);
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