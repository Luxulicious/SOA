using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(TransformReference))]
    public class
        TransformReferenceDrawer : ReferenceDrawer<TransformReference, TransformVariable, Transform, TransformUnityEvent, TransformTransformUnityEvent>
    {
    }
}