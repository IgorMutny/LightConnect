using UnityEngine;

namespace LightConnect.Audio
{
    [CreateAssetMenu(menuName = nameof(AudioSettings))]
    public class AudioSettings : ScriptableObject
    {
        [field: SerializeField] public AudioClip ClickClip { get; private set; }
        [field: SerializeField] public AudioClip WinClip { get; private set; }
        [field: SerializeField] public AudioClip ButtonClip { get; private set; }
    }
}
