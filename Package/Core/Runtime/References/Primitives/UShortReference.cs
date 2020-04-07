using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class UShortReference : Reference<UShortVariable, ushort, UShortUnityEvent, UShortUShortUnityEvent>
    {
        public UShortReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public UShortReference(IRegisteredReferenceContainer registration, ushort value) : base(registration, value)
        {
        }
    }
}