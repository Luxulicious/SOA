using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CreateAssetMenu(fileName = "New KeyCode List", menuName = "SOA/KeyCode/List", order = 1)]
    public class KeyCodeReferenceListVariable : ReferenceListVariable<KeyCodeReferenceList, KeyCodeReference, KeyCodeVariable, KeyCode,
        KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent, KeyCodeReferenceUnityEvent>
    {
    }
}