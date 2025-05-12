using UnityEngine;

namespace LightConnect.LevelConstruction
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

        private Constructor _constructor;

        private void Start()
        {
            _constructor = new Constructor(_levelView);

            _mainPanel.Initialize(_constructor);
            _sizePanel.Initialize(_constructor);
            _elementsPanel.Initialize(_constructor);
            _wiresPanel.Initialize(_constructor);
            _colorsPanel.Initialize(_constructor);
            _rotationsPanel.Initialize(_constructor);
        }

        private void OnDestroy()
        {
            _constructor.Clear();
        }
    }
}