using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector3Int Event", menuName = "SOA/UnityEngine/Vector3Int/Event", order = 1)]
    public class Vector3IntGameEvent : GameEvent<Vector3IntUnityEvent, Vector3Int>
    {
    }
}