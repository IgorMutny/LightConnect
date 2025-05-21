using LightConnect.Core;
using UnityEngine;

namespace LightConnect.Construction
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private TilesPanel _tilesPanel;
        [SerializeField] private WiresPanel _wiresPanel;
        [SerializeField] private ColorsPanel _colorsPanel;
        [SerializeField] private RotationsPanel _rotationsPanel;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private ConstructorView _constructorView;

        private ConstructorPresenter _constructorPresenter;

        private void Start()
        {
            var constructor = new Constructor();
            _constructorPresenter = new ConstructorPresenter(constructor, _constructorView, _levelView);
            constructor.CreateNewLevel();

            _mainPanel.Initialize(constructor);
            _tilesPanel.Initialize(constructor);
            _wiresPanel.Initialize(constructor);
            _colorsPanel.Initialize(constructor);
            _rotationsPanel.Initialize(constructor);
        }

        private void OnDestroy()
        {
            _constructorPresenter.Dispose();
        }
    }
}