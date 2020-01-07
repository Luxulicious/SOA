using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(CharReferenceListVariable))]
    public class CharReferenceListVariableEditor : VariableReferenceListEditor<CharReferenceListVariable,
        CharReferenceList,
        CharReference, CharVariable, char, CharUnityEvent, CharCharUnityEvent, CharReferenceUnityEvent>
    {
    }
}