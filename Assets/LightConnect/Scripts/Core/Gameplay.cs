using LightConnect.Model;
using R3;

namespace LightConnect.Core
{
    public class Gameplay
    {
        private CompositeDisposable _disposables = new();
        private Level _level;
        private LevelPresenter _levelPresenter;

        public Gameplay(LevelData levelData, LevelView levelView)
        {
            _level = new Level();
            _level.SetData(levelData);
            _level.Randomize();
            _levelPresenter = new LevelPresenter(_level, levelView);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _levelPresenter.Dispose();
        }
    }
}