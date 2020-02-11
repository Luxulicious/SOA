using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TransformUnityEvent : UnityEvent<Transform>
    {
    }

    [Serializable]
    public class TransformTransformUnityEvent : UnityEvent<Transform, Transform>
    {
    }

    [Serializable]
    public class TransformReferenceUnityEvent : UnityEvent<TransformReference>
    {
    }
}