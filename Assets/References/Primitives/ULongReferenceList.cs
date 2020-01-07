using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ULongReferenceList : ReferenceList<ULongReference, ULongVariable, ulong, ULongUnityEvent, ULongULongUnityEvent
        , ULongReferenceUnityEvent>
    {
    }
}