using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    public class FloatReferenceComponent : ReferenceComponent<FloatReference, FloatVariable, float, FloatUnityEvent, FloatFloatUnityEvent>
    {
        public void Decrement(float value)
        {
            _reference.Value -= value;
        }

        public void Increment(float value)
        {
            _reference.Value += value;
        }
    }
}