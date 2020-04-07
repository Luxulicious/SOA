using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TouchReference : Reference<TouchVariable, Touch, TouchUnityEvent, TouchTouchUnityEvent>
    {
        public TouchReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public TouchReference(IRegisteredReferenceContainer registration, Touch value) : base(registration, value)
        {
        }
    }
}