using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Rigidbody Event", menuName = "SOA/UnityEngine/Components/Rigidbody/Event", order = 1)]
    public class RigidbodyGameEvent : GameEvent<RigidbodyUnityEvent, Rigidbody>
    {
    }
}