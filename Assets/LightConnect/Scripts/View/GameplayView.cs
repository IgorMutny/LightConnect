using System;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.View
{
    public class GameplayView : MonoBehaviour
    {
        [field: SerializeField] public LevelView Level { get; private set; }

        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private WinText _winText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _hintButton;

        public event Action NextButtonClicked;
        public event Action HintButtonClicked;

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

        private void OnNextButtonClicked()
        {
            NextButtonClicked?.Invoke();
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

        private void OnHintButtonClicked()
        {
            HintButtonClicked?.Invoke();
        }
    }
}