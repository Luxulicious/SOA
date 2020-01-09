using System;
using UnityEngine.Events;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ByteUnityEvent : UnityEvent<byte>
    {
    }

    [Serializable]
    public class ByteByteUnityEvent : UnityEvent<byte, byte>
    {
    }

    [Serializable]
    public class ByteReferenceUnityEvent : UnityEvent<ByteReference>
    {
    }
}