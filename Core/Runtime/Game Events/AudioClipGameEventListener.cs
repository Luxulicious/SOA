using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AudioClipGameEventListener : GameEventListener<AudioClipGameEvent, AudioClipUnityEvent, AudioClip>
    {
    }
}