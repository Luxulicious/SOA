using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class FloatUnityEvent : UnityEvent<float>
    {
    }

    [Serializable]
    public class FloatFloatUnityEvent : UnityEvent<float, float>
    {
    }

    [Serializable]
    public class FloatReferenceUnityEvent : UnityEvent<FloatReference>
    {
    }
}