using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(QuaternionReferenceListVariable))]
    public class QuaternionReferenceListVariableEditor : VariableReferenceListEditor<QuaternionReferenceListVariable,
        QuaternionReferenceList,
        QuaternionReference, QuaternionVariable, Quaternion, QuaternionUnityEvent, QuaternionQuaternionUnityEvent, QuaternionReferenceUnityEvent>
    {
    }
}