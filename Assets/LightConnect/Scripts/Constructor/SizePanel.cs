using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class SizePanel : MonoBehaviour
    {
        [SerializeField] private Slider _width;
        [SerializeField] private Slider _height;

        private ReactiveProperty<int> _widthValue;
        private ReactiveProperty<int> _heightValue;


        public ReadOnlyReactiveProperty<int> Width => _widthValue;
        public ReadOnlyReactiveProperty<int> Height => _heightValue;

        public void Initialize()
        {
            _widthValue = new ReactiveProperty<int>((int)_width.value);
            _heightValue = new ReactiveProperty<int>((int)_height.value);

            _width.onValueChanged.AddListener(OnWidthChanged);
            _height.onValueChanged.AddListener(OnHeightChanged);
        }

        public void Dispose()
        {
            _width.onValueChanged.RemoveListener(OnWidthChanged);
            _height.onValueChanged.RemoveListener(OnHeightChanged);
        }

        private void OnWidthChanged(float value)
        {
            _widthValue.Value = (int)value;
        }

        private void OnHeightChanged(float value)
        {
            _heightValue.Value = (int)value;
        }
    }
}