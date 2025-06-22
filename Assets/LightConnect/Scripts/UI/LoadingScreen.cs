using System.Text;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace LightConnect.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField, Tooltip("in milliseconds")] private int _dotAmountChangeInterval;
        [SerializeField] private int _maxDotAmount;
        [SerializeField] private string _loadingMessage;

        private int _dotAmount;

        public void Show()
        {
            gameObject.SetActive(true);
            _dotAmount = 0;
            _text.text = CreateMessage();
            _ = ChangeDotsAmount();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private async UniTask ChangeDotsAmount()
        {
            while (gameObject.activeInHierarchy)
            {
                await UniTask.Delay(_dotAmountChangeInterval);

                if (_maxDotAmount > 0)
                {
                    _dotAmount += 1;

                    if (_dotAmount > _maxDotAmount)
                        _dotAmount -= _maxDotAmount;

                    _text.text = CreateMessage();
                }
            }
        }

        private string CreateMessage()
        {
            var builder = new StringBuilder();
            builder.Append(_loadingMessage);

            for (int i = 0; i <= _dotAmount; i++)
                builder.Append(".");

            return builder.ToString();
        }
    }
}