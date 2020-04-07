using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ObjectReference : Reference<ObjectVariable, object, ObjectUnityEvent, ObjectObjectUnityEvent>
    {
        public ObjectReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public ObjectReference(IRegisteredReferenceContainer registration, object value) : base(registration, value)
        {
        }
    }
}