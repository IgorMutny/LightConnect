using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.LevelConstruction
{
    public class SliderDescription : MonoBehaviour
    {
        [SerializeField] private string _title;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(_slider.value);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            _text.text = $"{_title}: {value}";
        }
    }
}
