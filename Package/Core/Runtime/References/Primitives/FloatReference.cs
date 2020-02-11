using System;
using SOA.Base;
using System;

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
    }
}