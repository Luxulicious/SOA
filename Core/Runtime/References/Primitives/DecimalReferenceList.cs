using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class DecimalReferenceList : ReferenceList<DecimalReference, DecimalVariable, decimal, DecimalUnityEvent, DecimalDecimalUnityEvent
        , DecimalReferenceUnityEvent>
    {
    }
}