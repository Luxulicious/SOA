using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(CharVariable))]
    public class CharVariableEditor : VariableEditor<CharVariable, char, CharUnityEvent, CharCharUnityEvent>
    {

    }
}