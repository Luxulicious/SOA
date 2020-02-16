using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(RigidbodyReferenceListVariable))]
    public class RigidbodyReferenceListVariableEditor : VariableReferenceListEditor<RigidbodyReferenceListVariable,
        RigidbodyReferenceList,
        RigidbodyReference, RigidbodyVariable, Rigidbody, RigidbodyUnityEvent, RigidbodyRigidbodyUnityEvent, RigidbodyReferenceUnityEvent>
    {
    }
}