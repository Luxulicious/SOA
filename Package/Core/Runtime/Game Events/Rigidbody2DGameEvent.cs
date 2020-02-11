using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Rigidbody2D Event", menuName = "SOA/UnityEngine/Components/Rigidbody2D/Event", order = 1)]
    public class Rigidbody2DGameEvent : GameEvent<Rigidbody2DUnityEvent, Rigidbody2D>
    {
    }
}