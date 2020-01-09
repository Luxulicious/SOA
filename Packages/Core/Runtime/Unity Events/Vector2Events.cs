using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector2UnityEvent : UnityEvent<Vector2>
    {
    }

    [Serializable]
    public class Vector2Vector2UnityEvent : UnityEvent<Vector2, Vector2>
    {
    }

    [Serializable]
    public class Vector2ReferenceUnityEvent : UnityEvent<Vector2Reference>
    {
    }
}