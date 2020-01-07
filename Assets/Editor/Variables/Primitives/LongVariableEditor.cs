using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(LongVariable))]
    public class LongVariableEditor : VariableEditor<LongVariable, long, LongUnityEvent, LongLongUnityEvent>
    {

    }
}