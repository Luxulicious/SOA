using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TouchReference : Reference<TouchVariable, Touch, TouchUnityEvent, TouchTouchUnityEvent>
    {
    }
}