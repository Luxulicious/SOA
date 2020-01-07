using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector3Variable))]
    public class Vector3VariableEditor : VariableEditor<Vector3Variable, Vector3, Vector3UnityEvent, Vector3Vector3UnityEvent>
    {

    }
}