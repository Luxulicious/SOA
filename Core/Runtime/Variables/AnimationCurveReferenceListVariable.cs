using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New AnimationCurve List", menuName = "SOA/UnityEngine/AnimationCurve/List", order = 1)]
    public class AnimationCurveReferenceListVariable : ReferenceListVariable<AnimationCurveReferenceList, AnimationCurveReference, AnimationCurveVariable, AnimationCurve,
        AnimationCurveUnityEvent, AnimationCurveAnimationCurveUnityEvent, AnimationCurveReferenceUnityEvent>
    {
    }
}