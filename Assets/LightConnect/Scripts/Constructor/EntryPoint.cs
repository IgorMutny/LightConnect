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

        private LevelPresenter _levelPresenter;

        private void Start()
        {
            var size = new Vector2Int(Level.MAX_SIZE / 2, Level.MAX_SIZE / 2);
            var level = new Level(size);

            _levelPresenter = new LevelPresenter(level, _levelView);

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
    }
}