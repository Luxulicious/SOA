using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(UShortVariable))]
    public class UShortVariableEditor : VariableEditor<UShortVariable, ushort, UShortUnityEvent, UShortUShortUnityEvent>
    {

    }
}