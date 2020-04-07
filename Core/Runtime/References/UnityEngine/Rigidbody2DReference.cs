using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Rigidbody2DReference : Reference<Rigidbody2DVariable, Rigidbody2D, Rigidbody2DUnityEvent, Rigidbody2DRigidbody2DUnityEvent>
    {
        public Rigidbody2DReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public Rigidbody2DReference(IRegisteredReferenceContainer registration, Rigidbody2D value) : base(registration, value)
        {
        }
    }
}