using LightConnect.View;
using LightConnect.Infrastructure;
using UnityEngine;

namespace LightConnect.Construction
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ConstructorView _constructorView;

        private ConstructorPresenter _constructorPresenter;

        private void Start()
        {
            GameMode.Current = GameMode.Mode.CONSTRUCTOR;
            var constructor = new Constructor();
            _constructorPresenter = new ConstructorPresenter(constructor, _constructorView);
        }

        private void OnDestroy()
        {
            _constructorPresenter.Dispose();
        }
    }
}