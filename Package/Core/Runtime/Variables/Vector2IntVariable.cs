using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector2Int Variable", menuName = "SOA/UnityEngine/Vector2Int/Variable", order = 1)]
    public class Vector2IntVariable : Variable<Vector2Int, Vector2IntUnityEvent, Vector2IntVector2IntUnityEvent>
    {
    }
}