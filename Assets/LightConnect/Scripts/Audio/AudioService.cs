using System.Collections.Generic;
using UnityEngine;

namespace LightConnect.Audio
{
    public class AudioService
    {
        private List<AudioSource> _audioSources = new();
        private AudioSource _musicSource;
        private AudioSettings _audioSettings;
        private bool _gameplaySoundsEnabled;
        private Transform _audioSourcesParent;
        private float _soundVolume;

        public AudioService(AudioSettings audioSettings)
        {
            if (Instance == null)
                Instance = this;
            else
                throw new System.Exception("Audio service has been already created");

            _audioSettings = audioSettings;
            _audioSourcesParent = new GameObject("AudioSources").transform;
            CreateMusicSource();
        }

        public static AudioService Instance { get; private set; }

        public void SetSoundVolume(float value)
        {
            _soundVolume = value;

            foreach (var source in _audioSources)
                source.volume = value;
        }

        public void SetMusicVolume(float value)
        {
            _musicSource.volume = value;
        }

        public void EnableGameplaySounds()
        {
            _gameplaySoundsEnabled = true;
        }

        public void DisableGameplaySounds()
        {
            _gameplaySoundsEnabled = false;
        }

        public void Pause()
        {
            _musicSource.Pause();

            foreach (var source in _audioSources)
                source.Pause();
        }

        public void Resume()
        {
            _musicSource.UnPause();

            foreach (var source in _audioSources)
                source.UnPause();
        }

        public void PlayClickSound()
        {
            if (!_gameplaySoundsEnabled)
                return;

            PlaySound(_audioSettings.ClickClip);
        }

        public void PlayLampSound()
        {
            if (!_gameplaySoundsEnabled)
                return;

            PlaySound(_audioSettings.LampClip);
        }

        public void PlayWinSound()
        {
            PlaySound(_audioSettings.WinClip);
        }

        public void PlayButtonSound()
        {
            PlaySound(_audioSettings.ButtonClip);
        }

        private void PlaySound(AudioClip clip)
        {
            if (!TryPlaySoundOnFreeSource(clip))
                PlaySoundOnNewSource(clip);
        }

        private bool TryPlaySoundOnFreeSource(AudioClip clip)
        {
            foreach (var source in _audioSources)
            {
                if (!source.isPlaying)
                {
                    source.PlayOneShot(clip);
                    return true;
                }
            }

            return false;
        }

        private void PlaySoundOnNewSource(AudioClip clip)
        {
            var audioSourceObject = new GameObject("source");
            audioSourceObject.transform.SetParent(_audioSourcesParent.transform);
            var audioSource = audioSourceObject.AddComponent<AudioSource>();
            _audioSources.Add(audioSource);
            audioSource.volume = _soundVolume;
            audioSource.PlayOneShot(clip);
        }

        private void CreateMusicSource()
        {
            _musicSource = new GameObject("music").AddComponent<AudioSource>();
            _musicSource.transform.SetParent(_audioSourcesParent);
            _musicSource.clip = _audioSettings.Music;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }
}