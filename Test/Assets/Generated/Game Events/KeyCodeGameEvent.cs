using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CreateAssetMenu(fileName = "New KeyCode Event", menuName = "SOA/KeyCode/Event", order = 1)]
    public class KeyCodeGameEvent : GameEvent<KeyCodeUnityEvent, KeyCode>
    {
    }
}