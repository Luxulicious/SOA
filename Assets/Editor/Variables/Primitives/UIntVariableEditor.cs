using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(UIntVariable))]
    public class UIntVariableEditor : VariableEditor<UIntVariable, uint, UIntUnityEvent, UIntUIntUnityEvent>
    {

    }
}