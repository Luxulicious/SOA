using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(AudioClipVariable))]
    public class AudioClipVariableEditor : VariableEditor<AudioClipVariable, AudioClip, AudioClipUnityEvent, AudioClipAudioClipUnityEvent>
    {

    }
}