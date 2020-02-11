using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Rigidbody2DVariable))]
    public class Rigidbody2DVariableEditor : VariableEditor<Rigidbody2DVariable, Rigidbody2D, Rigidbody2DUnityEvent, Rigidbody2DRigidbody2DUnityEvent>
    {

    }
}