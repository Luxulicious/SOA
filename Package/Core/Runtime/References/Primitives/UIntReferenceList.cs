using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class UIntReferenceList : ReferenceList<UIntReference, UIntVariable, uint, UIntUnityEvent, UIntUIntUnityEvent
        , UIntReferenceUnityEvent>
    {
    }
}