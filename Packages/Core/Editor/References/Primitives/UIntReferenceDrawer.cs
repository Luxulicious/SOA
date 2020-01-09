using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(UIntReference))]
    public class
        UIntReferenceDrawer : ReferenceDrawer<UIntReference, UIntVariable, uint, UIntUnityEvent, UIntUIntUnityEvent>
    {
    }
}