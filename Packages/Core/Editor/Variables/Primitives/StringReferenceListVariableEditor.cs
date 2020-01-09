using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(StringReferenceListVariable))]
    public class StringReferenceListVariableEditor : VariableReferenceListEditor<StringReferenceListVariable,
        StringReferenceList,
        StringReference, StringVariable, string, StringUnityEvent, StringStringUnityEvent, StringReferenceUnityEvent>
    {
    }
}