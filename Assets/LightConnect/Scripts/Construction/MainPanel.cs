using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Construction
{
    public class MainPanel : Panel
    {
        private const string ALREADY_EXISTS_MESSAGE = "Level with this id already exists!";
        private const string INVALID_ID_MESSAGE = "Invalid id!";
        private const int INVALID_ID = -1;

        [SerializeField] private Button _new;
        [SerializeField] private Button _load;
        [SerializeField] private Button _save;
        [SerializeField] private TMP_InputField _levelNumberInputField;
        [SerializeField] private TextMeshProUGUI _warning;

        private int _levelId = INVALID_ID;

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

        private void SetLevelName(string stringId)
        {
            bool result = int.TryParse(stringId, out int levelId);

            if (result)
            {
                _levelId = levelId;

                if (Constructor.LevelExists(_levelId))
                    _warning.text = ALREADY_EXISTS_MESSAGE;
                else
                    _warning.text = string.Empty;
            }
            else
            {
                _levelId = INVALID_ID;
                _warning.text = INVALID_ID_MESSAGE;
            }
        }

        private void Save()
        {
            if (_levelId != INVALID_ID)
                Constructor.Save(_levelId);
            else
                _warning.text = INVALID_ID_MESSAGE;
        }

        private void Load()
        {
            if (_levelId != INVALID_ID)
                Constructor.Load(_levelId);
            else
                _warning.text = INVALID_ID_MESSAGE;
        }

        private void New()
        {
            Constructor.ClearAndCreateNewLevel();
        }
    }
}
