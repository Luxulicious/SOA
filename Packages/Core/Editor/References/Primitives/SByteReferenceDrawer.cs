using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(SByteReference))]
    public class
        SByteReferenceDrawer : ReferenceDrawer<SByteReference, SByteVariable, sbyte, SByteUnityEvent, SByteSByteUnityEvent>
    {
    }
}