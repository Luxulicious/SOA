using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector3IntVariable))]
    public class Vector3IntVariableEditor : VariableEditor<Vector3IntVariable, Vector3Int, Vector3IntUnityEvent, Vector3IntVector3IntUnityEvent>
    {

    }
}