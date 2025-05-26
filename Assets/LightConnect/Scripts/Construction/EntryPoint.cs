using LightConnect.Core;
using LightConnect.Infrastructure;
using UnityEngine;

namespace LightConnect.Construction
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private TilesPanel _tilesPanel;
        [SerializeField] private WiresPanel _wiresPanel;
        [SerializeField] private ColorsPanel _colorsPanel;
        [SerializeField] private ActionsPanel _actionsPanel;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private ConstructorView _constructorView;

        private ConstructorPresenter _constructorPresenter;

        private void Start()
        {
            GameMode.Current = GameMode.Mode.CONSTRUCTOR;
            var constructor = new Constructor();
            _constructorPresenter = new ConstructorPresenter(constructor, _constructorView, _levelView);
            constructor.CreateNewLevel();

            _mainPanel.Initialize(constructor);
            _tilesPanel.Initialize(constructor);
            _wiresPanel.Initialize(constructor);
            _colorsPanel.Initialize(constructor);
            _actionsPanel.Initialize(constructor);
        }

        private void OnDestroy()
        {
            _constructorPresenter.Dispose();
        }
    }
}