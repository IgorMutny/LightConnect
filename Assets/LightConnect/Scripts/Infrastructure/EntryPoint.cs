using LightConnect.Model;
using LightConnect.View;
using UnityEngine;

namespace LightConnect.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameplayView _gameplayView;

        private GameplayPresenter _gameplayPresenter;

        private void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            Application.targetFrameRate = 60;
            GameMode.Current = GameMode.Mode.GAMEPLAY;
            
            var gameplay = new Gameplay();
            _gameplayPresenter = new GameplayPresenter(gameplay, _gameplayView);
            gameplay.Run();
        }

        private void OnDestroy()
        {
            _gameplayPresenter.Dispose();
        }
    }
}