using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(StringVariable))]
    public class StringVariableEditor : VariableEditor<StringVariable, string, StringUnityEvent, StringStringUnityEvent>
    {

    }
}