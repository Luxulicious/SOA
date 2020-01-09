using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(ObjectReferenceListVariable))]
    public class ObjectReferenceListVariableEditor : VariableReferenceListEditor<ObjectReferenceListVariable,
        ObjectReferenceList,
        ObjectReference, ObjectVariable, object, ObjectUnityEvent, ObjectObjectUnityEvent, ObjectReferenceUnityEvent>
    {
    }
}