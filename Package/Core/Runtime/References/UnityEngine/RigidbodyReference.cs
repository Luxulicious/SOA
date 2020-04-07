using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class RigidbodyReference : Reference<RigidbodyVariable, Rigidbody, RigidbodyUnityEvent, RigidbodyRigidbodyUnityEvent>
    {
        public RigidbodyReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public RigidbodyReference(IRegisteredReferenceContainer registration, Rigidbody value) : base(registration, value)
        {
        }
    }
}