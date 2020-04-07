using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector3Reference : Reference<Vector3Variable, Vector3, Vector3UnityEvent, Vector3Vector3UnityEvent>
    {
        public Vector3Reference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public Vector3Reference(IRegisteredReferenceContainer registration, Vector3 value) : base(registration, value)
        {
        }
    }
}