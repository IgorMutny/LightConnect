using LightConnect.LevelConstruction;
using UnityEngine;

namespace LightConnect.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LevelView _levelView;

        private void Start()
        {
            var game = new Game();
            game.LevelView = _levelView;
            game.CreateGameplay();
        }
    }
}