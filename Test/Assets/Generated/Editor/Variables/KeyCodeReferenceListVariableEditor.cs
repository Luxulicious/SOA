using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.CustomTypes
{

    [CustomEditor(typeof(KeyCodeReferenceListVariable))]
    public class KeyCodeReferenceListVariableEditor : VariableReferenceListEditor<KeyCodeReferenceListVariable,
        KeyCodeReferenceList,
        KeyCodeReference, KeyCodeVariable, KeyCode, KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent, KeyCodeReferenceUnityEvent>
    {
    }
}