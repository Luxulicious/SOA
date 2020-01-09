using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(LongReferenceListVariable))]
    public class LongReferenceListVariableEditor : VariableReferenceListEditor<LongReferenceListVariable,
        LongReferenceList,
        LongReference, LongVariable, long, LongUnityEvent, LongLongUnityEvent, LongReferenceUnityEvent>
    {
    }
}