using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector4UnityEvent : UnityEvent<Vector4>
    {
    }

    [Serializable]
    public class Vector4Vector4UnityEvent : UnityEvent<Vector4, Vector4>
    {
    }

    [Serializable]
    public class Vector4ReferenceUnityEvent : UnityEvent<Vector4Reference>
    {
    }
}