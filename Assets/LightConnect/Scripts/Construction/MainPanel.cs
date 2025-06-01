using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Construction
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private Button _new;
        [SerializeField] private Button _load;
        [SerializeField] private Button _save;
        [SerializeField] private TMP_InputField _levelIdInputField;
        [SerializeField] private TextMeshProUGUI _warning;

        public event Action<string> LevelIdChanged;
        public event Action SaveRequired;
        public event Action LoadRequired;
        public event Action ClearRequired;

        private void Start()
        {
            _levelIdInputField.onValueChanged.AddListener(SetLevelName);
            _new.onClick.AddListener(Clear);
            _save.onClick.AddListener(Save);
            _load.onClick.AddListener(Load);
        }

        private void OnDestroy()
        {
            _levelIdInputField.onValueChanged.RemoveListener(SetLevelName);
            _new.onClick.RemoveListener(Clear);
            _save.onClick.RemoveListener(Save);
            _load.onClick.RemoveListener(Load);
        }

        public void SetWarningText(string text)
        {
            _warning.text = text;
        }

        private void SetLevelName(string stringId)
        {
            LevelIdChanged?.Invoke(stringId);
        }

        private void Save()
        {
            SaveRequired?.Invoke();
        }

        private void Load()
        {
            LoadRequired?.Invoke();
        }

        private void Clear()
        {
            ClearRequired?.Invoke();
        }
    }
}
