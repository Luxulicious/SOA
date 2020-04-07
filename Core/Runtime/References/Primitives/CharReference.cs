using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class CharReference : Reference<CharVariable, char, CharUnityEvent, CharCharUnityEvent>
    {
        public CharReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public CharReference(IRegisteredReferenceContainer registration, char value) : base(registration, value)
        {
        }
    }
}