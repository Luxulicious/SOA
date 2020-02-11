using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(KeyCodeReference))]
    public class
        KeyCodeReferenceDrawer : ReferenceDrawer<KeyCodeReference, KeyCodeVariable, KeyCode, KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent>
    {
    }
}