using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : ReferenceDrawer<IntReference, IntVariable, int, IntUnityEvent, IntIntUnityEvent>
    {
    }
}