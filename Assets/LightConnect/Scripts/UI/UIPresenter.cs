using LightConnect.Audio;
using LightConnect.Model;
using LightConnect.Tutorial;

namespace LightConnect.UI
{
    public class UIPresenter
    {
        private Gameplay _gameplay;
        private UIView _view;

        public UIPresenter(Gameplay gameplay, UIView view)
        {
            _gameplay = gameplay;
            _view = view;

            _gameplay.LevelCreated += OnLevelCreated;
            _gameplay.LevelWon += OnLevelWon;
            _gameplay.LevelLoadingStarted += OnLevelLoadingStarted;
            _gameplay.LevelReady += OnLevelReady;
            _gameplay.TutorialRequired += OnTutorialRequired;
            _gameplay.OptionsInitialized += OnOptionsInitialized;

            _view.NextButtonClicked += LoadNextLevel;
            _view.HintButtonClicked += Help;
            _view.Options.SoundVolumeChanged += OnSoundVolumeChanged;
            _view.Options.MusicVolumeChanged += OnMusicVolumeChanged;
        }

        public void Dispose()
        {
            _gameplay.LevelCreated -= OnLevelCreated;
            _gameplay.LevelWon -= OnLevelWon;
            _gameplay.LevelLoadingStarted -= OnLevelLoadingStarted;
            _gameplay.LevelReady -= OnLevelReady;
            _gameplay.TutorialRequired -= OnTutorialRequired;
            _gameplay.OptionsInitialized -= OnOptionsInitialized;

            _view.NextButtonClicked -= LoadNextLevel;
            _view.HintButtonClicked -= Help;
            _view.Options.SoundVolumeChanged -= OnSoundVolumeChanged;
            _view.Options.MusicVolumeChanged -= OnMusicVolumeChanged;
        }

        private void OnLevelLoadingStarted()
        {
            _view.HideWinEffects();
            _view.ShowLoadingScreen();
        }

        private void OnLevelCreated(Level level)
        {
            _view.SetLevelId(_gameplay.CurrentLevelId);
            _view.ShowHintButton();
        }

        private void OnLevelReady()
        {
            _view.HideLoadingScreen();
            AudioService.Instance?.EnableGameplaySounds();
        }

        private void OnLevelWon()
        {
            _view.ShowWinEffects();
            _view.HideHintButton();
            AudioService.Instance?.DisableGameplaySounds();
        }

        private void LoadNextLevel()
        {
            _gameplay.RequestLoadNextLevel();
        }

        private void Help()
        {
            _gameplay.Help();
        }

        private void OnTutorialRequired(TutorialMessage message)
        {
            _view.ShowTutorialScreen(message);
        }

        private void OnOptionsInitialized(float soundVolume, float musicVolume)
        {
            _view.Options.SetInitialValues(soundVolume, musicVolume);
        }

        private void OnSoundVolumeChanged(float value)
        {
            _gameplay.SetSoundVolume(value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            _gameplay.SetMusicVolume(value);
        }
    }
}