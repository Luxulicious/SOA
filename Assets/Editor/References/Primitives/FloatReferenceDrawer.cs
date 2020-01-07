using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class
        FloatReferenceDrawer : ReferenceDrawer<FloatReference, FloatVariable, float ,FloatUnityEvent, FloatFloatUnityEvent>
    {
    }
}