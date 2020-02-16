using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(ColorReference))]
    public class
        ColorReferenceDrawer : ReferenceDrawer<ColorReference, ColorVariable, Color, ColorUnityEvent, ColorColorUnityEvent>
    {
    }
}