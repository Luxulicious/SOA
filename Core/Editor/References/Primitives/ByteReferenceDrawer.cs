using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(ByteReference))]
    public class
        ByteReferenceDrawer : ReferenceDrawer<ByteReference, ByteVariable, byte, ByteUnityEvent, ByteByteUnityEvent>
    {
    }
}