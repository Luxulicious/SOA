using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(TransformReferenceListVariable))]
    public class TransformReferenceListVariableEditor : VariableReferenceListEditor<TransformReferenceListVariable,
        TransformReferenceList,
        TransformReference, TransformVariable, Transform, TransformUnityEvent, TransformTransformUnityEvent, TransformReferenceUnityEvent>
    {
    }
}