using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(ColorVariable))]
    public class ColorVariableEditor : VariableEditor<ColorVariable, Color, ColorUnityEvent, ColorColorUnityEvent>
    {

    }
}