using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class DecimalUnityEvent : UnityEvent<decimal>
    {
    }

    [Serializable]
    public class DecimalDecimalUnityEvent : UnityEvent<decimal, decimal>
    {
    }

    [Serializable]
    public class DecimalReferenceUnityEvent : UnityEvent<DecimalReference>
    {
    }
}