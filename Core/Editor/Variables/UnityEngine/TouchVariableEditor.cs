using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(TouchVariable))]
    public class TouchVariableEditor : VariableEditor<TouchVariable, Touch, TouchUnityEvent, TouchTouchUnityEvent>
    {

    }
}