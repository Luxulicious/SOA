using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(ObjectReference))]
    public class
        ObjectReferenceDrawer : ReferenceDrawer<ObjectReference, ObjectVariable, object, ObjectUnityEvent, ObjectObjectUnityEvent>
    {
    }
}