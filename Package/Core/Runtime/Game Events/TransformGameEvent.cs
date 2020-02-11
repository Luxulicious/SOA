using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Transform Event", menuName = "SOA/UnityEngine/Components/Transform/Event", order = 1)]
    public class TransformGameEvent : GameEvent<TransformUnityEvent, Transform>
    {
    }
}