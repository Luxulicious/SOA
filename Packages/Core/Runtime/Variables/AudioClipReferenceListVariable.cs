using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New AudioClip List", menuName = "SOA/UnityEngine/AudioClip/List", order = 1)]
    public class AudioClipReferenceListVariable : ReferenceListVariable<AudioClipReferenceList, AudioClipReference, AudioClipVariable, AudioClip,
        AudioClipUnityEvent, AudioClipAudioClipUnityEvent, AudioClipReferenceUnityEvent>
    {
    }
}