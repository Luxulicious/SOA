using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class StringUnityEvent : UnityEvent<string>
    {
    }

    [Serializable]
    public class StringStringUnityEvent : UnityEvent<string, string>
    {
    }

    [Serializable]
    public class StringReferenceUnityEvent : UnityEvent<StringReference>
    {
    }
}