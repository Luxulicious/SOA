using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(UShortReferenceListVariable))]
    public class UShortReferenceListVariableEditor : VariableReferenceListEditor<UShortReferenceListVariable,
        UShortReferenceList,
        UShortReference, UShortVariable, ushort, UShortUnityEvent, UShortUShortUnityEvent, UShortReferenceUnityEvent>
    {
    }
}