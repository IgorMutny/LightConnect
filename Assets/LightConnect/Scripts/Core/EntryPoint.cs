using LightConnect.LevelConstruction;
using UnityEngine;

namespace LightConnect.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LevelView _levelView;

        private Gameplay _gameplay;

        private void Start()
        {
            var LevelSaveLoader = new LevelSaveLoader();
            var levelData = LevelSaveLoader.Load(0);
            _gameplay = new Gameplay(levelData, _levelView);
        }
    }
}