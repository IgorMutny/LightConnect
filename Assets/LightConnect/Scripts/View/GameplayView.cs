using System;
using System.Runtime.CompilerServices;
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
        [SerializeField] private WinText _winText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _hintButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private TextMeshProUGUI _levelNumber;

        public event Action NextButtonClicked;
        public event Action HintButtonClicked;

        private void Start()
        {
            HideOptionsScreen();
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

        public void ShowWinText()
        {
            _winText.Show();
        }

        public void HideWinText()
        {
            _winText.Hide();
        }

        public void ShowNextButton()
        {
            _nextButton.gameObject.SetActive(true);
            _nextButton.onClick.AddListener(OnNextButtonClicked);
        }

        public void HideNextButton()
        {
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
            NextButtonClicked?.Invoke();
        }

        private void OnHintButtonClicked()
        {
            HintButtonClicked?.Invoke();
        }

        private void ShowOptionsScreen()
        {
            _optionsScreen.gameObject.SetActive(true);
            _optionsScreen.CloseRequired += HideOptionsScreen;
        }

        private void HideOptionsScreen()
        {
            _optionsScreen.CloseRequired -= HideOptionsScreen;
            _optionsScreen.gameObject.SetActive(false);
        }
    }
}