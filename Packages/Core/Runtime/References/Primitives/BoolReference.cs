using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class BoolReference : Reference<BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent>
    {
    }
}