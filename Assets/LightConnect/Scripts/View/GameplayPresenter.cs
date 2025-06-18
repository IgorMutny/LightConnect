using LightConnect.Model;
using LightConnect.Tutorial;

namespace LightConnect.View
{
    public class GameplayPresenter
    {
        private Gameplay _model;
        private GameplayView _view;

        private LevelPresenter _levelPresenter;

        public GameplayPresenter(Gameplay model, GameplayView view)
        {
            _model = model;
            _view = view;

            _model.LevelCreated += OnLevelCreated;
            _model.LevelCompleted += OnLevelCompleted;
            _model.LevelLoadingStarted += OnLevelLoadingStarted;
            _model.LevelLoaded += OnLevelLoaded;
            _model.TutorialRequired += OnTutorialRequired;

            _view.NextButtonClicked += LoadNextLevel;
            _view.HintButtonClicked += Help;
        }

        public void Dispose()
        {
            DisposeLevel();

            _model.LevelCreated -= OnLevelCreated;
            _model.LevelCompleted -= OnLevelCompleted;
            _model.LevelLoadingStarted -= OnLevelLoadingStarted;
            _model.LevelLoaded -= OnLevelLoaded;
            _model.TutorialRequired -= OnTutorialRequired;

            _view.NextButtonClicked -= LoadNextLevel;
            _view.HintButtonClicked -= Help;
        }

        private void OnLevelLoadingStarted()
        {
            _view.HideWinEffects();

            _view.ShowLoadingScreen();

            DisposeLevel();
        }

        private void OnLevelCreated(Level level)
        {
            _levelPresenter = new LevelPresenter(level, _view.Level);
            _view.SetLevelNumber(_model.CurrentLevelNumber);
            _view.ShowHintButton();
        }

        private void OnLevelLoaded()
        {
            _view.HideLoadingScreen();
            _levelPresenter.AllowSounds();
        }

        private void OnLevelCompleted()
        {
            _view.ShowWinEffects();
            _view.HideHintButton();
        }

        private void DisposeLevel()
        {
            _levelPresenter?.Dispose();
            _levelPresenter = null;
        }

        private void LoadNextLevel()
        {
            _model.RequestLoadNextLevel();
        }

        private void Help()
        {
            _model.Help();
        }

        private void OnTutorialRequired(TutorialMessage message)
        {
            _view.ShowTutorialScreen(message);
        }
    }
}