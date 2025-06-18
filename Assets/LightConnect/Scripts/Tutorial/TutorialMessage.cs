using System;
using UnityEngine;

namespace LightConnect.Tutorial
{
    [Serializable]
    public class TutorialMessage
    {
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField] public string Header { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
    }
}