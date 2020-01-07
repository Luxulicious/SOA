using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ByteReferenceList : ReferenceList<ByteReference, ByteVariable, byte, ByteUnityEvent, ByteByteUnityEvent
        , ByteReferenceUnityEvent>
    {
    }
}