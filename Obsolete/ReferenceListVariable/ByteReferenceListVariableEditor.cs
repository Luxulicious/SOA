using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(ByteReferenceListVariable))]
    public class ByteReferenceListVariableEditor : VariableReferenceListEditor<ByteReferenceListVariable,
        ByteReferenceList,
        ByteReference, ByteVariable, byte, ByteUnityEvent, ByteByteUnityEvent, ByteReferenceUnityEvent>
    {
    }
}