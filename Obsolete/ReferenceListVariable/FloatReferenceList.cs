using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class FloatReferenceList : ReferenceList<FloatReference, FloatVariable, float ,FloatUnityEvent, FloatFloatUnityEvent
        , FloatReferenceUnityEvent>
    {
    }
}