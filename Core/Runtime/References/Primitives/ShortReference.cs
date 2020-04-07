using System;
using SOA.Base;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ShortReference : Reference<ShortVariable, short, ShortUnityEvent, ShortShortUnityEvent>
    {
        public ShortReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public ShortReference(IRegisteredReferenceContainer registration, short value) : base(registration, value)
        {
        }
    }
}