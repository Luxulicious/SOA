using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ByteGameEventListener : GameEventListener<ByteGameEvent, ByteUnityEvent, byte>
    {
    }
}