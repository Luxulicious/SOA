using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AnimationCurveReference : Reference<AnimationCurveVariable, AnimationCurve, AnimationCurveUnityEvent, AnimationCurveAnimationCurveUnityEvent>
    {
        public AnimationCurveReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public AnimationCurveReference(IRegisteredReferenceContainer registration, AnimationCurve value) : base(registration, value)
        {
        }
    }
}