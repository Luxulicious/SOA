using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AudioClipReferenceList : ReferenceList<AudioClipReference, AudioClipVariable, AudioClip, AudioClipUnityEvent, AudioClipAudioClipUnityEvent
        , AudioClipReferenceUnityEvent>
    {
    }
}