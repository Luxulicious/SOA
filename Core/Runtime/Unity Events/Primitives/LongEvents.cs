using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class LongUnityEvent : UnityEvent<long>
    {
    }

    [Serializable]
    public class LongLongUnityEvent : UnityEvent<long, long>
    {
    }

    [Serializable]
    public class LongReferenceUnityEvent : UnityEvent<LongReference>
    {
    }
}