using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class SizePanel : Panel
    {
        [SerializeField] private Slider _width;
        [SerializeField] private Slider _height;

        protected override void Subscribe()
        {
            _width.value = LevelPresenter.CurrentSize.x;
            _height.value = LevelPresenter.CurrentSize.y;

            _width.onValueChanged.AddListener(OnWidthChanged);
            _height.onValueChanged.AddListener(OnHeightChanged);
        }

        protected override void Unsubscribe()
        {
            _width.onValueChanged.RemoveListener(OnWidthChanged);
            _height.onValueChanged.RemoveListener(OnHeightChanged);
        }

        private void OnWidthChanged(float value)
        {
            LevelPresenter.ChangeWidth((int)value);
        }

        private void OnHeightChanged(float value)
        {
            LevelPresenter.ChangeHeight((int)value);
        }
    }
}