using SOA.Base;
using UnityEditor;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(BoolVariable))]
    public class BoolVariableEditor : VariableEditor<BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent>
    {

    }
}