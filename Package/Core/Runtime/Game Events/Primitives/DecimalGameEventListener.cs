using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class DecimalGameEventListener : GameEventListener<DecimalGameEvent, DecimalUnityEvent, decimal>
    {
    }
}