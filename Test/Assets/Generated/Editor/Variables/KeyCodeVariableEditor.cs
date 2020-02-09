using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CustomEditor(typeof(KeyCodeVariable))]
    public class KeyCodeVariableEditor : VariableEditor<KeyCodeVariable, KeyCode, KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent>
    {

    }
}