using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(AudioClipReferenceListVariable))]
    public class AudioClipReferenceListVariableEditor : VariableReferenceListEditor<AudioClipReferenceListVariable,
        AudioClipReferenceList,
        AudioClipReference, AudioClipVariable, AudioClip, AudioClipUnityEvent, AudioClipAudioClipUnityEvent, AudioClipReferenceUnityEvent>
    {
    }
}