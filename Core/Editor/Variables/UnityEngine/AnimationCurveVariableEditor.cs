using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(AnimationCurveVariable))]
    public class AnimationCurveVariableEditor : VariableEditor<AnimationCurveVariable, AnimationCurve, AnimationCurveUnityEvent, AnimationCurveAnimationCurveUnityEvent>
    {

    }
}