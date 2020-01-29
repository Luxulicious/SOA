using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(AnimationCurveGameEvent))]
    public class AnimationCurveGameEventEditor : GameEventEditor<AnimationCurveGameEvent, AnimationCurveUnityEvent, AnimationCurve>
    {
    }
}