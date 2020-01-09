using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(StringReference))]
    public class
        StringReferenceDrawer : ReferenceDrawer<StringReference, StringVariable, string, StringUnityEvent, StringStringUnityEvent>
    {
    }
}