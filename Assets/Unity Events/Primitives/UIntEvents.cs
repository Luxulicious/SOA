using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class UIntUnityEvent : UnityEvent<uint>
    {
    }

    [Serializable]
    public class UIntUIntUnityEvent : UnityEvent<uint, uint>
    {
    }

    [Serializable]
    public class UIntReferenceUnityEvent : UnityEvent<UIntReference>
    {
    }
}