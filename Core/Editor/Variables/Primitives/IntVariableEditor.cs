using SOA.Base;
using UnityEditor;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(IntVariable))]
    public class IntVariableEditor : VariableEditor<IntVariable, int, IntUnityEvent, IntIntUnityEvent>
    {
    }
}