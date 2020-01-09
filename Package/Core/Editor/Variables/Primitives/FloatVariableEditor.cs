using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableEditor : VariableEditor<FloatVariable, float, FloatUnityEvent, FloatFloatUnityEvent>
    {
    }
}