using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(Vector4ReferenceListVariable))]
    public class Vector4ReferenceListVariableEditor : VariableReferenceListEditor<Vector4ReferenceListVariable,
        Vector4ReferenceList,
        Vector4Reference, Vector4Variable, Vector4, Vector4UnityEvent, Vector4Vector4UnityEvent, Vector4ReferenceUnityEvent>
    {
    }
}