using SOA.Base;

namespace SOA.Common.Primitives
{
    public class
        IntReferenceComponent : ReferenceComponent<IntReference, IntVariable, int, IntUnityEvent, IntIntUnityEvent>
    {
        public void Decrement(int i)
        {
            _reference.Value -= i;
        }

        public void Increment(int i)
        {
            _reference.Value += i;
        }

        public int Value
        {
            get => _reference.Value;
            set => _reference.Value = value;
        }
    }
}