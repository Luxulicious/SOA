using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New AnimationCurve Event", menuName = "SOA/UnityEngine/AnimationCurve/Event", order = 1)]
    public class AnimationCurveGameEvent : GameEvent<AnimationCurveUnityEvent, AnimationCurve>
    {
    }
}