using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(SByteReferenceListVariable))]
    public class SByteReferenceListVariableEditor : VariableReferenceListEditor<SByteReferenceListVariable,
        SByteReferenceList,
        SByteReference, SByteVariable, sbyte, SByteUnityEvent, SByteSByteUnityEvent, SByteReferenceUnityEvent>
    {
    }
}