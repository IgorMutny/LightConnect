using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private Button _new;
        [SerializeField] private Button _load;
        [SerializeField] private Button _save;
        [SerializeField] private Button _clear;
        [SerializeField] private TMP_InputField _levelNumber;
        [SerializeField] private TextMeshProUGUI _warning;

        public void Initialize()
        {

        }

        public void Dispose()
        {

        }
    }
}
