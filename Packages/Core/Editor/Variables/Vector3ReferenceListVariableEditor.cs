using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(Vector3ReferenceListVariable))]
    public class Vector3ReferenceListVariableEditor : VariableReferenceListEditor<Vector3ReferenceListVariable,
        Vector3ReferenceList,
        Vector3Reference, Vector3Variable, Vector3, Vector3UnityEvent, Vector3Vector3UnityEvent, Vector3ReferenceUnityEvent>
    {
    }
}