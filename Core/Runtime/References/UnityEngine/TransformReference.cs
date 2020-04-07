using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TransformReference : Reference<TransformVariable, Transform, TransformUnityEvent, TransformTransformUnityEvent>
    {
        public TransformReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public TransformReference(IRegisteredReferenceContainer registration, Transform value) : base(registration, value)
        {
        }
    }
}