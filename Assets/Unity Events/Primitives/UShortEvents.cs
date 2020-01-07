using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class UShortUnityEvent : UnityEvent<ushort>
    {
    }

    [Serializable]
    public class UShortUShortUnityEvent : UnityEvent<ushort, ushort>
    {
    }

    [Serializable]
    public class UShortReferenceUnityEvent : UnityEvent<UShortReference>
    {
    }
}