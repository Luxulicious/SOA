using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(UShortReference))]
    public class
        UShortReferenceDrawer : ReferenceDrawer<UShortReference, UShortVariable, ushort, UShortUnityEvent, UShortUShortUnityEvent>
    {
    }
}