using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class LongGameEventListener : GameEventListener<LongGameEvent, LongUnityEvent, long>
    {
    }
}