using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(AnimationCurveReference))]
    public class
        AnimationCurveReferenceDrawer : ReferenceDrawer<AnimationCurveReference, AnimationCurveVariable, AnimationCurve, AnimationCurveUnityEvent, AnimationCurveAnimationCurveUnityEvent>
    {
    }
}