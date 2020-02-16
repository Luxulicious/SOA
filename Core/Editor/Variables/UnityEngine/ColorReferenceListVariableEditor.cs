using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(ColorReferenceListVariable))]
    public class ColorReferenceListVariableEditor : VariableReferenceListEditor<ColorReferenceListVariable,
        ColorReferenceList,
        ColorReference, ColorVariable, Color, ColorUnityEvent, ColorColorUnityEvent, ColorReferenceUnityEvent>
    {
    }
}