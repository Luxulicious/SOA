using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector3 Event", menuName = "SOA/UnityEngine/Vector3/Event", order = 1)]
    public class Vector3GameEvent : GameEvent<Vector3UnityEvent, Vector3>
    {
    }
}