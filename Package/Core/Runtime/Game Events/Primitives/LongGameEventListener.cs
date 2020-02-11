using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class LongGameEventListener : GameEventListener<LongGameEvent, LongUnityEvent, long>
    {
    }
}