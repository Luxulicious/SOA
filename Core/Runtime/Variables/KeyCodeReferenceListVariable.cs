using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New KeyCode List", menuName = "SOA/UnityEngine/KeyCode/List", order = 1)]
    public class KeyCodeReferenceListVariable : ReferenceListVariable<KeyCodeReferenceList, KeyCodeReference, KeyCodeVariable, KeyCode,
        KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent, KeyCodeReferenceUnityEvent>
    {
    }
}