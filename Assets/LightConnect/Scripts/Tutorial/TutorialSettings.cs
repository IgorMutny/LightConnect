using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LightConnect.Tutorial
{
    [CreateAssetMenu(menuName = nameof(TutorialSettings))]
    public class TutorialSettings : ScriptableObject
    {
        [SerializeField] private List<TutorialMessage> _messages;

        public bool GetMessageForLevel(int levelId, out TutorialMessage message)
        {
            message = _messages.FirstOrDefault(x => x.Level == levelId);
            return message != null;
        }
    }
}