using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CreateAssetMenu(fileName = "New GameObject Event", menuName = "SOA/GameObject/Event", order = 1)]
    public class GameObjectGameEvent : GameEvent<GameObjectUnityEvent, GameObject>
    {
    }
}