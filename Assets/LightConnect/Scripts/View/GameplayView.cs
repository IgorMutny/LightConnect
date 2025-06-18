using System;
using LightConnect.Audio;
using LightConnect.Tutorial;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.View
{
    public class GameplayView : MonoBehaviour
    {
        private const string LEVEL_TEXT = "LEVEL ";

        [field: SerializeField] public LevelView Level { get; private set; }

        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private OptionsScreen _optionsScreen;
        [SerializeField] private TutorialScreen _tutorialScreen;
        [SerializeField] private WinText _winText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _hintButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private TextMeshProUGUI _levelNumber;

        public event Action NextButtonClicked;
        public event Action HintButtonClicked;

        private void Start()
        {
            _optionsScreen.Hide();
            _tutorialScreen.Hide();
            _optionsButton.onClick.AddListener(ShowOptionsScreen);
        }

        private void OnDestroy()
        {
            _optionsButton.onClick.RemoveListener(ShowOptionsScreen);
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.Show();
        }

        public void HideLoadingScreen()
        {
            _loadingScreen.Hide();
        }

        public void ShowTutorialScreen(TutorialMessage message)
        {
            _tutorialScreen.Initialize(message);
            _tutorialScreen.Show();
        }

        public void ShowWinEffects()
        {
            _winText.Show();

            _nextButton.gameObject.SetActive(true);
            _nextButton.onClick.AddListener(OnNextButtonClicked);

            AudioService.Instance?.PlayWinSound();
        }

        public void HideWinEffects()
        {
            _winText.Hide();

            _nextButton.onClick.RemoveListener(OnNextButtonClicked);
            _nextButton.gameObject.SetActive(false);
        }

        public void ShowHintButton()
        {
            _hintButton.gameObject.SetActive(true);
            _hintButton.onClick.AddListener(OnHintButtonClicked);
        }

        public void HideHintButton()
        {
            _hintButton.onClick.RemoveListener(OnHintButtonClicked);
            _hintButton.gameObject.SetActive(false);
        }

        public void SetLevelNumber(int number)
        {
            _levelNumber.text = LEVEL_TEXT + number.ToString();
        }

        private void OnNextButtonClicked()
        {
            AudioService.Instance?.PlayButtonSound();
            NextButtonClicked?.Invoke();
        }

        private void OnHintButtonClicked()
        {
            AudioService.Instance?.PlayButtonSound();
            HintButtonClicked?.Invoke();
        }

        private void ShowOptionsScreen()
        {
            AudioService.Instance?.PlayButtonSound();
            _optionsScreen.Show();
        }
    }
}