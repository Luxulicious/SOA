using SOA.Base;
using UnityEditor;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(IntReferenceListVariable))]
    public class IntReferenceListVariableEditor : VariableReferenceListEditor<IntReferenceListVariable, IntReferenceList
        ,
        IntReference, IntVariable, int, IntUnityEvent, IntIntUnityEvent, IntReferenceUnityEvent>
    {
    }
}