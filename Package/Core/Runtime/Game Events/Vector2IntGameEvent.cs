using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector2Int Event", menuName = "SOA/UnityEngine/Vector2Int/Event", order = 1)]
    public class Vector2IntGameEvent : GameEvent<Vector2IntUnityEvent, Vector2Int>
    {
    }
}