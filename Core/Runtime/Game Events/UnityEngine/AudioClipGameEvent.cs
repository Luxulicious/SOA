using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New AudioClip Event", menuName = "SOA/UnityEngine/AudioClip/Event", order = 1)]
    public class AudioClipGameEvent : GameEvent<AudioClipUnityEvent, AudioClip>
    {
    }
}