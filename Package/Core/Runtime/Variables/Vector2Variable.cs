using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector2 Variable", menuName = "SOA/UnityEngine/Vector2/Variable", order = 1)]
    public class Vector2Variable : Variable<Vector2, Vector2UnityEvent, Vector2Vector2UnityEvent>
    {
    }
}