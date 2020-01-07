using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AnimationCurveReference : Reference<AnimationCurveVariable, AnimationCurve, AnimationCurveUnityEvent, AnimationCurveAnimationCurveUnityEvent>
    {
    }
}