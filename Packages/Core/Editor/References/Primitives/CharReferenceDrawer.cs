using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(CharReference))]
    public class
        CharReferenceDrawer : ReferenceDrawer<CharReference, CharVariable, char, CharUnityEvent, CharCharUnityEvent>
    {
    }
}