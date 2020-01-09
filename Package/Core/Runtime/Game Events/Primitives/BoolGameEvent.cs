using SOA.Base;
using UnityEngine;


namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Bool Event", menuName = "SOA/Primitives/Bool/Event", order = 1)]
    public class BoolGameEvent : GameEvent<BoolUnityEvent, bool>
    {
    }
}