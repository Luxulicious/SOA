using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(ShortReference))]
    public class
        ShortReferenceDrawer : ReferenceDrawer<ShortReference, ShortVariable, short, ShortUnityEvent, ShortShortUnityEvent>
    {
    }
}