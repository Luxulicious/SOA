using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CreateAssetMenu(fileName = "New KeyCode Variable", menuName = "SOA/KeyCode/Variable", order = 1)]
    public class KeyCodeVariable : Variable<KeyCode, KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent>
    {
    }
}