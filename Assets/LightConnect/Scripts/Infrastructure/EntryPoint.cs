using LightConnect.Core;
using UnityEngine;

namespace LightConnect.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LevelView _levelView;

        private void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            GameMode.Current = GameMode.Mode.GAMEPLAY;
            var game = new Gameplay(_levelView);
            game.Run();
        }
    }
}