using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New AudioClip Variable", menuName = "SOA/UnityEngine/AudioClip/Variable", order = 1)]
    public class AudioClipVariable : Variable<AudioClip, AudioClipUnityEvent, AudioClipAudioClipUnityEvent>
    {
    }
}