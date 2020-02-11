using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(Vector3IntReferenceListVariable))]
    public class Vector3IntReferenceListVariableEditor : VariableReferenceListEditor<Vector3IntReferenceListVariable,
        Vector3IntReferenceList,
        Vector3IntReference, Vector3IntVariable, Vector3Int, Vector3IntUnityEvent, Vector3IntVector3IntUnityEvent, Vector3IntReferenceUnityEvent>
    {
    }
}