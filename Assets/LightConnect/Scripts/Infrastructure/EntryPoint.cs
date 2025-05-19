using LightConnect.Core;
using UnityEngine;

namespace LightConnect.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LevelView _levelView;

        private void Start()
        {
            var game = new Game(_levelView);
            game.Run();
        }
    }
}