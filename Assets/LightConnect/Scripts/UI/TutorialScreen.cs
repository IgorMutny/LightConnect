using LightConnect.Audio;
using LightConnect.Tutorial;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.UI
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] private Button _okButton;
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _image;

        public void Show()
        {
            gameObject.SetActive(true);
            _okButton.onClick.AddListener(PlaySoundAndHide);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(TutorialMessage message)
        {
            _header.text = message.Header;
            _description.text = message.Description;
            _image.sprite = message.Image;
        }

        private void PlaySoundAndHide()
        {
            AudioService.Instance?.PlayButtonSound();
            _okButton.onClick.RemoveListener(PlaySoundAndHide);
            Hide();
        }
    }
}
