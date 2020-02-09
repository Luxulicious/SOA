using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [Serializable]
    public class KeyCodeReferenceList : ReferenceList<KeyCodeReference, KeyCodeVariable, KeyCode, KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent
        , KeyCodeReferenceUnityEvent>
    {
    }
}