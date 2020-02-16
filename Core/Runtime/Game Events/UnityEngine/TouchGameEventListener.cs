using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TouchGameEventListener : GameEventListener<TouchGameEvent, TouchUnityEvent, Touch>
    {
    }
}