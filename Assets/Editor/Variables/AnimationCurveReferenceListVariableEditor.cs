using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(AnimationCurveReferenceListVariable))]
    public class AnimationCurveReferenceListVariableEditor : VariableReferenceListEditor<AnimationCurveReferenceListVariable,
        AnimationCurveReferenceList,
        AnimationCurveReference, AnimationCurveVariable, AnimationCurve, AnimationCurveUnityEvent, AnimationCurveAnimationCurveUnityEvent, AnimationCurveReferenceUnityEvent>
    {
    }
}