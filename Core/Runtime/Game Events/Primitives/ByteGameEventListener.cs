using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ByteGameEventListener : EventListener<ByteGameEvent, ByteUnityEvent, byte>
    {
    }
}