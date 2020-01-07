using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AnimationCurveUnityEvent : UnityEvent<AnimationCurve>
    {
    }

    [Serializable]
    public class AnimationCurveAnimationCurveUnityEvent : UnityEvent<AnimationCurve, AnimationCurve>
    {
    }

    [Serializable]
    public class AnimationCurveReferenceUnityEvent : UnityEvent<AnimationCurveReference>
    {
    }
}