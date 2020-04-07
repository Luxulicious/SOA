using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class UIntReference : Reference<UIntVariable, uint, UIntUnityEvent, UIntUIntUnityEvent>
    {
        public UIntReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public UIntReference(IRegisteredReferenceContainer registration, uint value) : base(registration, value)
        {
        }
    }
}