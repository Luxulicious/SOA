using System;
using UnityEngine.Events;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class BoolUnityEvent : UnityEvent<bool>
    {
    }

    [Serializable]
    public class BoolBoolUnityEvent : UnityEvent<bool, bool>
    {
    }

    [Serializable]
    public class BoolReferenceUnityEvent : UnityEvent<BoolReference>
    {
    }
}