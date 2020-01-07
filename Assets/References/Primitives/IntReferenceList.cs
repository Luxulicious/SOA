using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class IntReferenceList : ReferenceList<IntReference, IntVariable, int, IntUnityEvent, IntIntUnityEvent,
        IntReferenceUnityEvent>
    {
    }
}