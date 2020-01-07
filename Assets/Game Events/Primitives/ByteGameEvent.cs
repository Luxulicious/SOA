using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Byte Event", menuName = "SOA/Primitives/Byte/Event", order = 1)]
    public class ByteGameEvent : GameEvent<ByteUnityEvent, byte>
    {
    }
}