using LightConnect.Model;

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
            _model.DisposeLevelRequired += DisposeLevel;
            _model.ShowWinEffectsRequired += ShowWinEffects;
            _model.HideWinEffectsRequired += HideWinEffects;
            _model.ShowLoadingScreenRequired += ShowLoadingScreen;
            _model.HideLoadingScreenRequired += HideLoadingScreen;

            _view.NextButtonClicked += LoadNextLevel;
            _view.HintButtonClicked += Help;
        }

        public void Dispose()
        {
            DisposeLevel();
            _model.LevelCreated -= OnLevelCreated;
            _model.DisposeLevelRequired -= DisposeLevel;
            _model.ShowWinEffectsRequired -= ShowWinEffects;
            _model.HideWinEffectsRequired -= HideWinEffects;
            _model.ShowLoadingScreenRequired -= ShowLoadingScreen;
            _model.HideLoadingScreenRequired -= HideLoadingScreen;

            _view.NextButtonClicked -= LoadNextLevel;
            _view.HintButtonClicked -= Help;
        }

        private void OnLevelCreated(Level level)
        {
            _levelPresenter = new LevelPresenter(level, _view.Level);
            _view.ShowHintButton();
        }

        private void ShowWinEffects()
        {
            _view.ShowWinText();
            _view.ShowNextButton();
            _view.HideHintButton();
        }

        private void HideWinEffects()
        {
            _view.HideWinText();
            _view.HideNextButton();
        }

        private void DisposeLevel()
        {
            _levelPresenter?.Dispose();
            _levelPresenter = null;
        }

        private void ShowLoadingScreen()
        {
            _view.ShowLoadingScreen();
        }

        private void HideLoadingScreen()
        {
            _view.HideLoadingScreen();
        }

        private void LoadNextLevel()
        {
            _model.RequestLoadNextLevel();
        }

        private void Help()
        {
            _model.Help();
        }
    }
}