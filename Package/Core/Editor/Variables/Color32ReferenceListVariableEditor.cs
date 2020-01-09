using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(Color32ReferenceListVariable))]
    public class Color32ReferenceListVariableEditor : VariableReferenceListEditor<Color32ReferenceListVariable,
        Color32ReferenceList,
        Color32Reference, Color32Variable, Color32, Color32UnityEvent, Color32Color32UnityEvent, Color32ReferenceUnityEvent>
    {
    }
}