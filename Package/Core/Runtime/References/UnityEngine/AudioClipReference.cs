using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AudioClipReference : Reference<AudioClipVariable, AudioClip, AudioClipUnityEvent, AudioClipAudioClipUnityEvent>
    {
    }
}