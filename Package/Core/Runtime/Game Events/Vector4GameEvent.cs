using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector4 Event", menuName = "SOA/UnityEngine/Vector4/Event", order = 1)]
    public class Vector4GameEvent : GameEvent<Vector4UnityEvent, Vector4>
    {
    }
}