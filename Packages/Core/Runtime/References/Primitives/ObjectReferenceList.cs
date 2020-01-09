using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ObjectReferenceList : ReferenceList<ObjectReference, ObjectVariable, object, ObjectUnityEvent, ObjectObjectUnityEvent
        , ObjectReferenceUnityEvent>
    {
    }
}