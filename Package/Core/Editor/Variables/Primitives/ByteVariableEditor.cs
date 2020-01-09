using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(ByteVariable))]
    public class ByteVariableEditor : VariableEditor<ByteVariable, byte, ByteUnityEvent, ByteByteUnityEvent>
    {

    }
}