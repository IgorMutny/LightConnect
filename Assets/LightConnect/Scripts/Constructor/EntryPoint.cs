using UnityEngine;

namespace LightConnect.Constructor
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private ElementsPanel _elementsPanel;
        [SerializeField] private SizePanel _sizePanel;
        [SerializeField] private ColorsPanel _colorsPanel;
        [SerializeField] private ActionsPanel _actionsPanel;
        [SerializeField] private LevelView _levelView;

        private void Start()
        {
            _mainPanel.Initialize();
            _elementsPanel.Initialize();
            _sizePanel.Initialize();
            _colorsPanel.Initialize();
            _actionsPanel.Initialize();
            _levelView.Initialize();
        }

        private void OnDestroy()
        {
            _mainPanel.Dispose();
            _elementsPanel.Dispose();
            _sizePanel.Dispose();
            _colorsPanel.Dispose();
            _actionsPanel.Dispose();
            _levelView.Dispose();
        }

    }
}