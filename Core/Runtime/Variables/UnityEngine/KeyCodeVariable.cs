using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New KeyCode Variable", menuName = "SOA/UnityEngine/KeyCode/Variable", order = 1)]
    public class KeyCodeVariable : Variable<KeyCode, KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent>
    {
    }
}