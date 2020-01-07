using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(ShortReferenceListVariable))]
    public class ShortReferenceListVariableEditor : VariableReferenceListEditor<ShortReferenceListVariable,
        ShortReferenceList,
        ShortReference, ShortVariable, short, ShortUnityEvent, ShortShortUnityEvent, ShortReferenceUnityEvent>
    {
    }
}