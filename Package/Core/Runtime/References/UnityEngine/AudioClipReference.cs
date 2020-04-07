using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AudioClipReference : Reference<AudioClipVariable, AudioClip, AudioClipUnityEvent, AudioClipAudioClipUnityEvent>
    {
        public AudioClipReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public AudioClipReference(IRegisteredReferenceContainer registration, AudioClip value) : base(registration, value)
        {
        }
    }
}