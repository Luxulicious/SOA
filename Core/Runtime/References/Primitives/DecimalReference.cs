using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class DecimalReference : Reference<DecimalVariable, decimal, DecimalUnityEvent, DecimalDecimalUnityEvent>
    {
        public DecimalReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public DecimalReference(IRegisteredReferenceContainer registration, decimal value) : base(registration, value)
        {
        }
    }
}