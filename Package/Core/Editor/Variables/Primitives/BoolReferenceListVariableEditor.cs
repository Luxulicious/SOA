using SOA.Base;
using UnityEditor;

namespace SOA.Common.Primitives
{

    [CustomEditor(typeof(BoolReferenceListVariable))]
    public class BoolReferenceListVariableEditor : VariableReferenceListEditor<BoolReferenceListVariable,
        BoolReferenceList,
        BoolReference, BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent, BoolReferenceUnityEvent>
    {
    }
}