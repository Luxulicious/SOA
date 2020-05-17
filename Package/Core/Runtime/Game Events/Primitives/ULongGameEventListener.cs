using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ULongGameEventListener : GameEventListener<ULongGameEvent, ULongUnityEvent, ulong>
    {
    }
}