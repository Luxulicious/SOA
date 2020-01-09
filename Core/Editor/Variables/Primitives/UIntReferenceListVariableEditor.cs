using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(UIntReferenceListVariable))]
    public class UIntReferenceListVariableEditor : VariableReferenceListEditor<UIntReferenceListVariable,
        UIntReferenceList,
        UIntReference, UIntVariable, uint, UIntUnityEvent, UIntUIntUnityEvent, UIntReferenceUnityEvent>
    {
    }
}