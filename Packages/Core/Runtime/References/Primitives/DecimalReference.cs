using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class DecimalReference : Reference<DecimalVariable, decimal, DecimalUnityEvent, DecimalDecimalUnityEvent>
    {
    }
}