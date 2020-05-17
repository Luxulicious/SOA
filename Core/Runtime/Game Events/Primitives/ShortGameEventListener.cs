using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ShortGameEventListener : GameEventListener<ShortGameEvent, ShortUnityEvent, short>
    {
    }
}