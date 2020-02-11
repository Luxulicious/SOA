using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class RigidbodyReference : Reference<RigidbodyVariable, Rigidbody, RigidbodyUnityEvent, RigidbodyRigidbodyUnityEvent>
    {
    }
}