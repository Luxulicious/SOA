using SOA.Base;
using UnityEngine;


namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Int List", menuName = "SOA/Primitives/Int/List", order = 1)]
    public class IntReferenceListVariable : ReferenceListVariable<IntReferenceList, IntReference, IntVariable, int,
        IntUnityEvent, IntIntUnityEvent, IntReferenceUnityEvent>
    {
    }
}