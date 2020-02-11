using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(TransformVariable))]
    public class TransformVariableEditor : VariableEditor<TransformVariable, Transform, TransformUnityEvent, TransformTransformUnityEvent>
    {

    }
}