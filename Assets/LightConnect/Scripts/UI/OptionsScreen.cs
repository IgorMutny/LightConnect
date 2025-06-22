using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightConnect.UI
{
    public class OptionsScreen : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Slider _soundVolume;
        [SerializeField] private Slider _musicVolume;

        public event Action<float> SoundVolumeChanged;
        public event Action<float> MusicVolumeChanged;

        private void Start()
        {
            _soundVolume.onValueChanged.AddListener(OnSoundVolumeChanged);
            _musicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        private void OnDestroy()
        {
            _soundVolume.onValueChanged.RemoveListener(OnSoundVolumeChanged);
            _musicVolume.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject == gameObject)
            {
                Hide();
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetInitialValues(float soundVolume, float musicVolume)
        {
            _soundVolume.value = soundVolume;
            _musicVolume.value = musicVolume;
        }

        private void OnSoundVolumeChanged(float value)
        {
            SoundVolumeChanged?.Invoke(value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            MusicVolumeChanged?.Invoke(value);
        }
    }
}