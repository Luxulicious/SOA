using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ObjectReference : Reference<ObjectVariable, object, ObjectUnityEvent, ObjectObjectUnityEvent>
    {
    }
}