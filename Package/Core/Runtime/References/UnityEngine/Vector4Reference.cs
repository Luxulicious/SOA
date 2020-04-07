using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector4Reference : Reference<Vector4Variable, Vector4, Vector4UnityEvent, Vector4Vector4UnityEvent>
    {
        public Vector4Reference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public Vector4Reference(IRegisteredReferenceContainer registration, Vector4 value) : base(registration, value)
        {
        }
    }
}