using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector3 Variable", menuName = "SOA/UnityEngine/Vector3/Variable", order = 1)]
    public class Vector3Variable : Variable<Vector3, Vector3UnityEvent, Vector3Vector3UnityEvent>
    {
    }
}