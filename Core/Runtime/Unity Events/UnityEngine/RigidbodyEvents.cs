using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class RigidbodyUnityEvent : UnityEvent<Rigidbody>
    {
    }

    [Serializable]
    public class RigidbodyRigidbodyUnityEvent : UnityEvent<Rigidbody, Rigidbody>
    {
    }

    [Serializable]
    public class RigidbodyReferenceUnityEvent : UnityEvent<RigidbodyReference>
    {
    }
}