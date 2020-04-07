using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ByteReference : Reference<ByteVariable, byte, ByteUnityEvent, ByteByteUnityEvent>
    {
        public ByteReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public ByteReference(IRegisteredReferenceContainer registration, byte value) : base(registration, value)
        {
        }
    }
}