using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(AnimationCurveGameEvent))]
    public class AnimationCurveGameEventEditor : UnityEventSOEditor<AnimationCurveGameEvent, AnimationCurveUnityEvent, AnimationCurve>
    {
    }
}