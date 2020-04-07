using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(DecimalReferenceListVariable))]
    public class DecimalReferenceListVariableEditor : VariableReferenceListEditor<DecimalReferenceListVariable,
        DecimalReferenceList,
        DecimalReference, DecimalVariable, decimal, DecimalUnityEvent, DecimalDecimalUnityEvent, DecimalReferenceUnityEvent>
    {
    }
}