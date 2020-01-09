using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class IntReference : Reference<IntVariable, int, IntUnityEvent, IntIntUnityEvent>
    {
    }
}