using LightConnect.View;
using LightConnect.Infrastructure;
using UnityEngine;

namespace LightConnect.Construction
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LevelView _levelView;
        [SerializeField] private PlaceholdersView _placeholdersView;
        [SerializeField] private UIView _uiView;

        private LevelPresenter _levelPresenter;
        private PlaceholdersPresenter _placeholdersPresenter;
        private UIPresenter _uiPresenter;

        private void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            Application.targetFrameRate = 60;
            GameMode.Current = GameMode.Mode.CONSTRUCTOR;

            var constructor = new Constructor();

            _levelPresenter = new LevelPresenter(constructor, _levelView);
            _levelPresenter.SetDimensionSize(_placeholdersView.DimensionSize);
            _placeholdersPresenter = new PlaceholdersPresenter(constructor, _placeholdersView);
            _uiPresenter = new UIPresenter(constructor, _uiView);

            constructor.CreateNewLevel();
        }

        private void OnDestroy()
        {
            _levelPresenter.Dispose();
            _placeholdersPresenter.Dispose();
            _uiPresenter.Dispose();
        }
    }
}