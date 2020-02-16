using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(TouchReference))]
    public class
        TouchReferenceDrawer : ReferenceDrawer<TouchReference, TouchVariable, Touch, TouchUnityEvent, TouchTouchUnityEvent>
    {
    }
}