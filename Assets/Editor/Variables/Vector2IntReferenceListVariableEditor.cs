using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(Vector2IntReferenceListVariable))]
    public class Vector2IntReferenceListVariableEditor : VariableReferenceListEditor<Vector2IntReferenceListVariable,
        Vector2IntReferenceList,
        Vector2IntReference, Vector2IntVariable, Vector2Int, Vector2IntUnityEvent, Vector2IntVector2IntUnityEvent, Vector2IntReferenceUnityEvent>
    {
    }
}