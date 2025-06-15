using UnityEngine;

namespace LightConnect.Audio
{
    public class AudioService
    {
        private AudioSource _audioSource;
        private AudioSettings _audioSettings;

        public AudioService(AudioSettings audioSettings)
        {
            if (Instance == null)
                Instance = this;
            else
                throw new System.Exception("Audio service has been already created");

            _audioSettings = audioSettings;
            _audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
        }

        public static AudioService Instance { get; private set; }

        public void PlayClickSound()
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_audioSettings.ClickClip);
        }

        public void PlayWinSound()
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_audioSettings.WinClip);
        }

        public void PlayButtonSound()
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_audioSettings.ButtonClip);
        }
    }
}