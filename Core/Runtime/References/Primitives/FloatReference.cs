using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class FloatReference : Reference<FloatVariable, float, FloatUnityEvent, FloatFloatUnityEvent>
    {
        public FloatReference()
        {
        }

        public FloatReference(float value) : base(value)
        {
        }

        public FloatReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public FloatReference(IRegisteredReferenceContainer registration, float value) : base(registration, value)
        {
        }
    }
}