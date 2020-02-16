using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AnimationCurveReferenceList : ReferenceList<AnimationCurveReference, AnimationCurveVariable, AnimationCurve, AnimationCurveUnityEvent, AnimationCurveAnimationCurveUnityEvent
        , AnimationCurveReferenceUnityEvent>
    {
    }
}