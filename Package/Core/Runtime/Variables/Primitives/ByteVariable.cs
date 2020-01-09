using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Byte Variable", menuName = "SOA/Primitives/Byte/Variable", order = 1)]
    public class ByteVariable : Variable<byte, ByteUnityEvent, ByteByteUnityEvent>
    {
    }
}