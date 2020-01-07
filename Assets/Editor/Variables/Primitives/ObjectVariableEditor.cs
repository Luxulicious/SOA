using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(ObjectVariable))]
    public class ObjectVariableEditor : VariableEditor<ObjectVariable, object, ObjectUnityEvent, ObjectObjectUnityEvent>
    {

    }
}