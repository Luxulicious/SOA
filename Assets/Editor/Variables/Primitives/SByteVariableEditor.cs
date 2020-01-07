using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(SByteVariable))]
    public class SByteVariableEditor : VariableEditor<SByteVariable, sbyte, SByteUnityEvent, SByteSByteUnityEvent>
    {

    }
}