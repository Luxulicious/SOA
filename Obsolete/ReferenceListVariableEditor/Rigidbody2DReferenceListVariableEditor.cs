using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(Rigidbody2DReferenceListVariable))]
    public class Rigidbody2DReferenceListVariableEditor : VariableReferenceListEditor<Rigidbody2DReferenceListVariable,
        Rigidbody2DReferenceList,
        Rigidbody2DReference, Rigidbody2DVariable, Rigidbody2D, Rigidbody2DUnityEvent, Rigidbody2DRigidbody2DUnityEvent, Rigidbody2DReferenceUnityEvent>
    {
    }
}