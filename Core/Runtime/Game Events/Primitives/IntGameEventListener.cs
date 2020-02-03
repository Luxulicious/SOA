using System;
using System.Collections.Generic;
using System.Linq;
using SOA.Base;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class IntGameEventListener : GameEventListener<IntGameEvent, IntUnityEvent, int>
    {
    }
}