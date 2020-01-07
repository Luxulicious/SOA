using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(LongReference))]
    public class
        LongReferenceDrawer : ReferenceDrawer<LongReference, LongVariable, long, LongUnityEvent, LongLongUnityEvent>
    {
    }
}