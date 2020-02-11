using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New KeyCode Event", menuName = "SOA/UnityEngine/KeyCode/Event", order = 1)]
    public class KeyCodeGameEvent : GameEvent<KeyCodeUnityEvent, KeyCode>
    {
    }
}