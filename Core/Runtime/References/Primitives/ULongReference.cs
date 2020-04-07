using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ULongReference : Reference<ULongVariable, ulong, ULongUnityEvent, ULongULongUnityEvent>
    {
        public ULongReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public ULongReference(IRegisteredReferenceContainer registration, ulong value) : base(registration, value)
        {
        }
    }
}