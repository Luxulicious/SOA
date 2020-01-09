using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(ShortVariable))]
    public class ShortVariableEditor : VariableEditor<ShortVariable, short, ShortUnityEvent, ShortShortUnityEvent>
    {

    }
}