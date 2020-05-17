using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class SByteGameEventListener : GameEventListener<SByteGameEvent, SByteUnityEvent, sbyte>
    {
    }
}