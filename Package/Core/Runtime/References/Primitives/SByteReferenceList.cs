using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class SByteReferenceList : ReferenceList<SByteReference, SByteVariable, sbyte, SByteUnityEvent, SByteSByteUnityEvent
        , SByteReferenceUnityEvent>
    {
    }
}