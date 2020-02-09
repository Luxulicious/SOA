using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [Serializable]
    public class KeyCodeReference : Reference<KeyCodeVariable, KeyCode, KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent>
    {
    }
}