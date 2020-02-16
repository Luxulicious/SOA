using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(QuaternionVariable))]
    public class QuaternionVariableEditor : VariableEditor<QuaternionVariable, Quaternion, QuaternionUnityEvent, QuaternionQuaternionUnityEvent>
    {

    }
}