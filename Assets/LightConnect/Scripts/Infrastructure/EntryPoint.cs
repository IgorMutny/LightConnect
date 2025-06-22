using LightConnect.Audio;
using LightConnect.Model;
using LightConnect.Tutorial;
using LightConnect.UI;
using LightConnect.View;
using UnityEngine;
using AudioSettings = LightConnect.Audio.AudioSettings;

namespace LightConnect.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AudioSettings _audioSettings;
        [SerializeField] private TutorialSettings _tutorialSettings;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private UIView _uiView;

        private UIPresenter _uiPresenter;
        private LevelPresenter _levelPresenter;

        private void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            Application.targetFrameRate = 60;
            GameMode.Current = GameMode.Mode.GAMEPLAY;

            new AudioService(_audioSettings);
            new TutorialService(_tutorialSettings);

            var gameplay = new Gameplay();

            _uiPresenter = new UIPresenter(gameplay, _uiView);
            _levelPresenter = new LevelPresenter(gameplay, _levelView);

            gameplay.Run();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        private void OnDestroy()
        {
            _uiPresenter.Dispose();
            _levelPresenter.Dispose();
        }
    }
}