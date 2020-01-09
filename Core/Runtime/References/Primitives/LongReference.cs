using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class LongReference : Reference<LongVariable, long, LongUnityEvent, LongLongUnityEvent>
    {
    }
}