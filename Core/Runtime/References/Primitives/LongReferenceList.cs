using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class LongReferenceList : ReferenceList<LongReference, LongVariable, long, LongUnityEvent, LongLongUnityEvent
        , LongReferenceUnityEvent>
    {
    }
}