using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class KeyCodeUnityEvent : UnityEvent<KeyCode>
    {
    }

    [Serializable]
    public class KeyCodeKeyCodeUnityEvent : UnityEvent<KeyCode, KeyCode>
    {
    }

    [Serializable]
    public class KeyCodeReferenceUnityEvent : UnityEvent<KeyCodeReference>
    {
    }
}