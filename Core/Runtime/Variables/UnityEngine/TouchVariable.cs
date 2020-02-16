using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Touch Variable", menuName = "SOA/UnityEngine/Touch/Variable", order = 1)]
    public class TouchVariable : Variable<Touch, TouchUnityEvent, TouchTouchUnityEvent>
    {
    }
}