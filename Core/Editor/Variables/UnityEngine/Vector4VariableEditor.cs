using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector4Variable))]
    public class Vector4VariableEditor : VariableEditor<Vector4Variable, Vector4, Vector4UnityEvent, Vector4Vector4UnityEvent>
    {

    }
}