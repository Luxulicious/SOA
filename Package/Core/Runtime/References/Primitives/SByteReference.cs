using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class SByteReference : Reference<SByteVariable, sbyte, SByteUnityEvent, SByteSByteUnityEvent>
    {
        public SByteReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public SByteReference(IRegisteredReferenceContainer registration, sbyte value) : base(registration, value)
        {
        }
    }
}