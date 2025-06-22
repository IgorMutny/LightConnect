using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LightConnect.UI
{
    public class WinText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private string[] _winMessages;
        [SerializeField] private float _appearanceTime;

        public void Show()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(1f, _appearanceTime);

            if (_winMessages.Length == 0)
                return;

            var message = _winMessages[Random.Range(0, _winMessages.Length)];
            _winText.text = message;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}