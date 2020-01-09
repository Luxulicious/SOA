using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class SByteUnityEvent : UnityEvent<sbyte>
    {
    }

    [Serializable]
    public class SByteSByteUnityEvent : UnityEvent<sbyte, sbyte>
    {
    }

    [Serializable]
    public class SByteReferenceUnityEvent : UnityEvent<SByteReference>
    {
    }
}