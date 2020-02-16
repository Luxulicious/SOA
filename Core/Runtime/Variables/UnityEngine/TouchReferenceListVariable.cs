using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Touch List", menuName = "SOA/UnityEngine/Touch/List", order = 1)]
    public class TouchReferenceListVariable : ReferenceListVariable<TouchReferenceList, TouchReference, TouchVariable, Touch,
        TouchUnityEvent, TouchTouchUnityEvent, TouchReferenceUnityEvent>
    {
    }
}