using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Color32Variable))]
    public class Color32VariableEditor : VariableEditor<Color32Variable, Color32, Color32UnityEvent, Color32Color32UnityEvent>
    {

    }
}