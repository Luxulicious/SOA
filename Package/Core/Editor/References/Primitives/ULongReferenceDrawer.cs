using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(ULongReference))]
    public class
        ULongReferenceDrawer : ReferenceDrawer<ULongReference, ULongVariable, ulong, ULongUnityEvent, ULongULongUnityEvent>
    {
    }
}