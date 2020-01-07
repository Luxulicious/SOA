using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector2IntVariable))]
    public class Vector2IntVariableEditor : VariableEditor<Vector2IntVariable, Vector2Int, Vector2IntUnityEvent, Vector2IntVector2IntUnityEvent>
    {

    }
}