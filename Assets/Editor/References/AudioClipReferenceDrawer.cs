using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(AudioClipReference))]
    public class
        AudioClipReferenceDrawer : ReferenceDrawer<AudioClipReference, AudioClipVariable, AudioClip, AudioClipUnityEvent, AudioClipAudioClipUnityEvent>
    {
    }
}