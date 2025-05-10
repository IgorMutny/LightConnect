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

        private void Start()
        {
            _mainPanel.Initialize();
            _elementsPanel.Initialize();
            _wiresPanel.Initialize();
            _sizePanel.Initialize();
            _colorsPanel.Initialize();
            _rotationsPanel.Initialize();
            _levelView.Initialize();
        }

        private void OnDestroy()
        {
            _mainPanel.Dispose();
            _elementsPanel.Dispose();
            _wiresPanel.Dispose();
            _sizePanel.Dispose();
            _colorsPanel.Dispose();
            _rotationsPanel.Dispose();
            _levelView.Dispose();
        }

    }
}