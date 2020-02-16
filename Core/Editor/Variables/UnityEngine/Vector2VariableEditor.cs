using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector2Variable))]
    public class Vector2VariableEditor : VariableEditor<Vector2Variable, Vector2, Vector2UnityEvent, Vector2Vector2UnityEvent>
    {

    }
}