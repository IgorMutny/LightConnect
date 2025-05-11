using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class MainPanel : Panel
    {
        [SerializeField] private Button _new;
        [SerializeField] private Button _load;
        [SerializeField] private Button _save;
        [SerializeField] private Button _clear;
        [SerializeField] private TMP_InputField _levelNumber;
        [SerializeField] private TextMeshProUGUI _warning;

        protected override void Subscribe()
        {
            
        }

        protected override void Unsubscribe()
        {
            
        }
    }
}
