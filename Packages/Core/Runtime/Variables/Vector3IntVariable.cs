using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector3Int Variable", menuName = "SOA/UnityEngine/Vector3Int/Variable", order = 1)]
    public class Vector3IntVariable : Variable<Vector3Int, Vector3IntUnityEvent, Vector3IntVector3IntUnityEvent>
    {
    }
}