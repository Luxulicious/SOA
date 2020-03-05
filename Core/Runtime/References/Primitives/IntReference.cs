using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class IntReference : Reference<IntVariable, int, IntUnityEvent, IntIntUnityEvent>
    {
        public IntReference()
        {
        }

        public IntReference(int value) : base(value)
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