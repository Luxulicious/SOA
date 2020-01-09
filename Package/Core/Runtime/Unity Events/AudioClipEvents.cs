using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AudioClipUnityEvent : UnityEvent<AudioClip>
    {
    }

    [Serializable]
    public class AudioClipAudioClipUnityEvent : UnityEvent<AudioClip, AudioClip>
    {
    }

    [Serializable]
    public class AudioClipReferenceUnityEvent : UnityEvent<AudioClipReference>
    {
    }
}