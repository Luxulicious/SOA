using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(RigidbodyVariable))]
    public class RigidbodyVariableEditor : VariableEditor<RigidbodyVariable, Rigidbody, RigidbodyUnityEvent, RigidbodyRigidbodyUnityEvent>
    {

    }
}