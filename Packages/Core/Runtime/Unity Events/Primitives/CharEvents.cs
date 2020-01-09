using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class CharUnityEvent : UnityEvent<char>
    {
    }

    [Serializable]
    public class CharCharUnityEvent : UnityEvent<char, char>
    {
    }

    [Serializable]
    public class CharReferenceUnityEvent : UnityEvent<CharReference>
    {
    }
}