using System;
using SOA.Base;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class IntReference : Reference<IntVariable, int, IntUnityEvent, IntIntUnityEvent>
    {
        public IntReference()
        {
        }
        
        public IntReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public IntReference(IRegisteredReferenceContainer registration, int value) : base(registration, value)
        {
        }

        public void Decrement(int i)
        {
            Value -= i;
        }

        public void Increment(int i)
        {
            Value += i;
        }
    }
}