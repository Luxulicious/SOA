using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector2 Event", menuName = "SOA/UnityEngine/Vector2/Event", order = 1)]
    public class Vector2GameEvent : GameEvent<Vector2UnityEvent, Vector2>
    {
    }
}