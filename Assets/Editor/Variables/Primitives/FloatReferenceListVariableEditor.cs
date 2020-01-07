using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(FloatReferenceListVariable))]
    public class FloatReferenceListVariableEditor : VariableReferenceListEditor<FloatReferenceListVariable,
        FloatReferenceList,
        FloatReference, FloatVariable, float ,FloatUnityEvent, FloatFloatUnityEvent, FloatReferenceUnityEvent>
    {
    }
}