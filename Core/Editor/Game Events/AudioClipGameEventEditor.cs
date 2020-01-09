using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(AudioClipGameEvent))]
    public class AudioClipGameEventEditor : UnityEventSOEditor<AudioClipGameEvent, AudioClipUnityEvent, AudioClip>
    {
    }
}