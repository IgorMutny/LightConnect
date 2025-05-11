using LightConnect.Meta;
using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Constructor
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private ElementsPanel _elementsPanel;
        [SerializeField] private WiresPanel _wiresPanel;
        [SerializeField] private SizePanel _sizePanel;
        [SerializeField] private ColorsPanel _colorsPanel;
        [SerializeField] private RotationsPanel _rotationsPanel;
        [SerializeField] private LevelView _levelView;

        private Level _level;
        private LevelPresenter _levelPresenter;
        private SaveLoader _saveLoader;

        private void Start()
        {
            _saveLoader = new SaveLoader();

            var size = new Vector2Int(Level.MAX_SIZE / 2, Level.MAX_SIZE / 2);
            _level = new Level(size);

            _levelPresenter = new LevelPresenter(_level, _levelView);

            _mainPanel.Initialize(_levelPresenter);
            _sizePanel.Initialize(_levelPresenter);
            _elementsPanel.Initialize(_levelPresenter);
            _wiresPanel.Initialize(_levelPresenter);
            _colorsPanel.Initialize(_levelPresenter);
            _rotationsPanel.Initialize(_levelPresenter);
        }

        private void OnDestroy()
        {
            _levelPresenter.Dispose();
        }

        public void Save(int levelNumber)
        {
            _saveLoader.Save(_level, levelNumber);
        }

        public void Load(int levelNumber)
        {
            Clear();

            var levelData = _saveLoader.Load(levelNumber);
            var size = new Vector2Int(levelData.SizeX, levelData.SizeY);
            _level = new Level(size);
            _level.SetData(levelData);

            _levelPresenter = new LevelPresenter(_level, _levelView);

            _mainPanel.Reinitialize(_levelPresenter);
            _sizePanel.Reinitialize(_levelPresenter);
            _elementsPanel.Reinitialize(_levelPresenter);
            _wiresPanel.Reinitialize(_levelPresenter);
            _colorsPanel.Reinitialize(_levelPresenter);
            _rotationsPanel.Reinitialize(_levelPresenter);

        }

        public void New()
        {
            Clear();

            var size = new Vector2Int(Level.MAX_SIZE / 2, Level.MAX_SIZE / 2);
            _level = new Level(size);

            _levelPresenter = new LevelPresenter(_level, _levelView);

            _mainPanel.Reinitialize(_levelPresenter);
            _sizePanel.Reinitialize(_levelPresenter);
            _elementsPanel.Reinitialize(_levelPresenter);
            _wiresPanel.Reinitialize(_levelPresenter);
            _colorsPanel.Reinitialize(_levelPresenter);
            _rotationsPanel.Reinitialize(_levelPresenter);
        }

        private void Clear()
        {
            _levelView.Clear();
            _levelPresenter.Dispose();
            _levelPresenter = null;
            _level = null;
        }
    }
}