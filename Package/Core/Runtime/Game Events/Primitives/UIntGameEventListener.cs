using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class UIntGameEventListener : GameEventListener<UIntGameEvent, UIntUnityEvent, uint>
    {
    }
}