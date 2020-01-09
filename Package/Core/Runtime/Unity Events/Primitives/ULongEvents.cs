using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ULongUnityEvent : UnityEvent<ulong>
    {
    }

    [Serializable]
    public class ULongULongUnityEvent : UnityEvent<ulong, ulong>
    {
    }

    [Serializable]
    public class ULongReferenceUnityEvent : UnityEvent<ULongReference>
    {
    }
}