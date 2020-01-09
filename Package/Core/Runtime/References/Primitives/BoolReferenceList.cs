using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class BoolReferenceList : ReferenceList<BoolReference, BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent
        , BoolReferenceUnityEvent>
    {
    }
}