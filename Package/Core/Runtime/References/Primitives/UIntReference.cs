using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class UIntReference : Reference<UIntVariable, uint, UIntUnityEvent, UIntUIntUnityEvent>
    {
    }
}