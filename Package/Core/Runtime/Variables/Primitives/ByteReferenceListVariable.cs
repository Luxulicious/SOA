using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Byte List", menuName = "SOA/Primitives/Byte/List", order = 1)]
    public class ByteReferenceListVariable : ReferenceListVariable<ByteReferenceList, ByteReference, ByteVariable, byte,
        ByteUnityEvent, ByteByteUnityEvent, ByteReferenceUnityEvent>
    {
    }
}