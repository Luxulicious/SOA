using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Rigidbody2DUnityEvent : UnityEvent<Rigidbody2D>
    {
    }

    [Serializable]
    public class Rigidbody2DRigidbody2DUnityEvent : UnityEvent<Rigidbody2D, Rigidbody2D>
    {
    }

    [Serializable]
    public class Rigidbody2DReferenceUnityEvent : UnityEvent<Rigidbody2DReference>
    {
    }
}