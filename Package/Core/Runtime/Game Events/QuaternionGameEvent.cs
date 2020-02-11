using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Quaternion Event", menuName = "SOA/UnityEngine/Quaternion/Event", order = 1)]
    public class QuaternionGameEvent : GameEvent<QuaternionUnityEvent, Quaternion>
    {
    }
}