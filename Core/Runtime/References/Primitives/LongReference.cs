using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class LongReference : Reference<LongVariable, long, LongUnityEvent, LongLongUnityEvent>
    {
        public LongReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public LongReference(IRegisteredReferenceContainer registration, long value) : base(registration, value)
        {
        }
    }
}