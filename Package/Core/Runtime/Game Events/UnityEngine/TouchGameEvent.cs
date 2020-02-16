using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Touch Event", menuName = "SOA/UnityEngine/Touch/Event", order = 1)]
    public class TouchGameEvent : GameEvent<TouchUnityEvent, Touch>
    {
    }
}