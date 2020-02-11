using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector3UnityEvent : UnityEvent<Vector3>
    {
    }

    [Serializable]
    public class Vector3Vector3UnityEvent : UnityEvent<Vector3, Vector3>
    {
    }

    [Serializable]
    public class Vector3ReferenceUnityEvent : UnityEvent<Vector3Reference>
    {
    }
}