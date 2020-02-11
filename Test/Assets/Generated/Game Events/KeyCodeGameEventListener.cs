using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class KeyCodeGameEventListener : GameEventListener<KeyCodeGameEvent, KeyCodeUnityEvent, KeyCode>
    {
    }
}