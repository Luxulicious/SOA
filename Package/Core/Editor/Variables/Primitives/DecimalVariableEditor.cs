using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(DecimalVariable))]
    public class DecimalVariableEditor : VariableEditor<DecimalVariable, decimal, DecimalUnityEvent, DecimalDecimalUnityEvent>
    {

    }
}