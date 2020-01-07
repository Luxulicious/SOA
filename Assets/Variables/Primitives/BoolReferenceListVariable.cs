using SOA.Base;
using UnityEngine;


namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Bool List", menuName = "SOA/Primitives/Bool/List", order = 1)]
    public class BoolReferenceListVariable : ReferenceListVariable<BoolReferenceList, BoolReference, BoolVariable, bool,
        BoolUnityEvent, BoolBoolUnityEvent, BoolReferenceUnityEvent>
    {
    }
}