using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(ULongVariable))]
    public class ULongVariableEditor : VariableEditor<ULongVariable, ulong, ULongUnityEvent, ULongULongUnityEvent>
    {

    }
}