using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Bool Variable", menuName = "SOA/Primitives/Bool/Variable", order = 1)]
    public class BoolVariable : Variable<bool, BoolUnityEvent, BoolBoolUnityEvent>
    {
        
    }
}