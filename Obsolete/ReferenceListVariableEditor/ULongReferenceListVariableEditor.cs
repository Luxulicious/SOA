using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(ULongReferenceListVariable))]
    public class ULongReferenceListVariableEditor : VariableReferenceListEditor<ULongReferenceListVariable,
        ULongReferenceList,
        ULongReference, ULongVariable, ulong, ULongUnityEvent, ULongULongUnityEvent, ULongReferenceUnityEvent>
    {
    }
}