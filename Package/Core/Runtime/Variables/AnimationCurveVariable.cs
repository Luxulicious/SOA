using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New AnimationCurve Variable", menuName = "SOA/UnityEngine/AnimationCurve/Variable", order = 1)]
    public class AnimationCurveVariable : Variable<AnimationCurve, AnimationCurveUnityEvent, AnimationCurveAnimationCurveUnityEvent>
    {
    }
}