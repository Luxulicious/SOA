using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(Vector2ReferenceListVariable))]
    public class Vector2ReferenceListVariableEditor : VariableReferenceListEditor<Vector2ReferenceListVariable,
        Vector2ReferenceList,
        Vector2Reference, Vector2Variable, Vector2, Vector2UnityEvent, Vector2Vector2UnityEvent, Vector2ReferenceUnityEvent>
    {
    }
}