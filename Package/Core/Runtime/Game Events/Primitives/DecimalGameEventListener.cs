using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class DecimalGameEventListener : GameEventListener<DecimalGameEvent, DecimalUnityEvent, decimal>
    {
    }
}