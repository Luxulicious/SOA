using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ShortReferenceList : ReferenceList<ShortReference, ShortVariable, short, ShortUnityEvent, ShortShortUnityEvent
        , ShortReferenceUnityEvent>
    {
    }
}