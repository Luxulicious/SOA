using System;
using SOA.Base;
using System;
using Object = UnityEngine.Object;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class StringReference : Reference<StringVariable, string, StringUnityEvent, StringStringUnityEvent>
    {
        public StringReference()
        {
        }

        public StringReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public StringReference(IRegisteredReferenceContainer registration, string value) : base(registration, value)
        {
        }
    }
}