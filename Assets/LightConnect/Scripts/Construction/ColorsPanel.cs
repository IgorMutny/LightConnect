#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEngine.UI;
using Color = LightConnect.Model.Color;

namespace LightConnect.Construction
{
    public class ColorsPanel : MonoBehaviour
    {
        [SerializeField] private Button _red;
        [SerializeField] private Button _yellow;
        [SerializeField] private Button _blue;
        [SerializeField] private Button _orange;
        [SerializeField] private Button _green;
        [SerializeField] private Button _magenta;

        public event Action<Color> SetColorRequired;

        private void Start()
        {
            _red.onClick.AddListener(SelectRed);
            _yellow.onClick.AddListener(SelectYellow);
            _blue.onClick.AddListener(SelectBlue);
            _orange.onClick.AddListener(SelectOrange);
            _green.onClick.AddListener(SelectGreen);
            _magenta.onClick.AddListener(SelectMagenta);
        }

        private void OnDestroy()
        {
            _red.onClick.RemoveListener(SelectRed);
            _yellow.onClick.RemoveListener(SelectYellow);
            _blue.onClick.RemoveListener(SelectBlue);
            _orange.onClick.RemoveListener(SelectOrange);
            _green.onClick.RemoveListener(SelectGreen);
            _magenta.onClick.RemoveListener(SelectMagenta);
        }

        private void SelectColor(Color color)
        {
            SetColorRequired?.Invoke(color);
        }

        private void SelectYellow()
        {
            SelectColor(Color.Yellow);
        }

        private void SelectBlue()
        {
            SelectColor(Color.Blue);
        }

        private void SelectRed()
        {
            SelectColor(Color.Red);
        }

        private void SelectOrange()
        {
            SelectColor(Color.Orange);
        }

        private void SelectGreen()
        {
            SelectColor(Color.Green);
        }

        private void SelectMagenta()
        {
            SelectColor(Color.Magenta);
        }
    }
}

#endif