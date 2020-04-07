using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class QuaternionReference : Reference<QuaternionVariable, Quaternion, QuaternionUnityEvent, QuaternionQuaternionUnityEvent>
    {
        public QuaternionReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public QuaternionReference(IRegisteredReferenceContainer registration, Quaternion value) : base(registration, value)
        {
        }
    }
}