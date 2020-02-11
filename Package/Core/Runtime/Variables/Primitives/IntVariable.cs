using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Int Variable", menuName = "SOA/Primitives/Int/Variable", order = 1)]
    public class IntVariable : Variable<int, IntUnityEvent, IntIntUnityEvent>
    {
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