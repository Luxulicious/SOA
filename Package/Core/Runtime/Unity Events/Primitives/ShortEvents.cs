using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ShortUnityEvent : UnityEvent<short>
    {
    }

    [Serializable]
    public class ShortShortUnityEvent : UnityEvent<short, short>
    {
    }

    [Serializable]
    public class ShortReferenceUnityEvent : UnityEvent<ShortReference>
    {
    }
}