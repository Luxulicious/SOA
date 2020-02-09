using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CustomPropertyDrawer(typeof(KeyCodeReference))]
    public class
        KeyCodeReferenceDrawer : ReferenceDrawer<KeyCodeReference, KeyCodeVariable, KeyCode, KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent>
    {
    }
}