using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ObjectUnityEvent : UnityEvent<object>
    {
    }

    [Serializable]
    public class ObjectObjectUnityEvent : UnityEvent<object, object>
    {
    }

    [Serializable]
    public class ObjectReferenceUnityEvent : UnityEvent<ObjectReference>
    {
    }
}