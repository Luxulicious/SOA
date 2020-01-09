using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(DecimalReference))]
    public class
        DecimalReferenceDrawer : ReferenceDrawer<DecimalReference, DecimalVariable, decimal, DecimalUnityEvent, DecimalDecimalUnityEvent>
    {
    }
}