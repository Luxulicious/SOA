﻿using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class BoolGameEventListener : GameEventListener<BoolGameEvent, BoolUnityEvent, bool>
    {
    }
}