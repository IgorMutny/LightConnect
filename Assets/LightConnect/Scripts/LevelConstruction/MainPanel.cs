using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.LevelConstruction
{
    public class MainPanel : Panel
    {
        private const string NOT_NUMBER_MESSAGE = "Not number!";

        [SerializeField] private Button _new;
        [SerializeField] private Button _load;
        [SerializeField] private Button _save;
        [SerializeField] private TMP_InputField _levelNumberInputField;
        [SerializeField] private TextMeshProUGUI _warning;

        private int _levelNumber;

        protected override void Subscribe()
        {
            _levelNumberInputField.onValueChanged.AddListener(SetLevelName);
            _new.onClick.AddListener(New);
            _save.onClick.AddListener(Save);
            _load.onClick.AddListener(Load);
        }

        protected override void Unsubscribe()
        {
            _levelNumberInputField.onValueChanged.RemoveListener(SetLevelName);
            _new.onClick.RemoveListener(New);
            _save.onClick.RemoveListener(Save);
            _load.onClick.RemoveListener(Load);
        }

        private void SetLevelName(string number)
        {
            bool result = int.TryParse(number, out int levelNumber);

            if (result)
            {
                _levelNumber = levelNumber;
                _warning.text = string.Empty;
            }
            else
            {
                _warning.text = NOT_NUMBER_MESSAGE;
            }
        }

        private void Save()
        {
            Constructor.Save(_levelNumber);
        }

        private void Load()
        {
            Constructor.Load(_levelNumber);
        }

        private void New()
        {
            Constructor.CreateNewLevel();
        }
    }
}
