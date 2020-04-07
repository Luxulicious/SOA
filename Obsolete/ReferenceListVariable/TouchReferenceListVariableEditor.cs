using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(TouchReferenceListVariable))]
    public class TouchReferenceListVariableEditor : VariableReferenceListEditor<TouchReferenceListVariable,
        TouchReferenceList,
        TouchReference, TouchVariable, Touch, TouchUnityEvent, TouchTouchUnityEvent, TouchReferenceUnityEvent>
    {
    }
}