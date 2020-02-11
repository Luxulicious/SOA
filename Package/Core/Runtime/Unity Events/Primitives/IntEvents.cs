using System;
using UnityEngine.Events;


namespace SOA.Common.Primitives
{
    [Serializable]
    public class IntUnityEvent : UnityEvent<int>
    {
    }

    [Serializable]
    public class IntIntUnityEvent : UnityEvent<int, int>
    {
    }

    [Serializable]
    public class IntReferenceUnityEvent : UnityEvent<IntReference>
    {
    }
}