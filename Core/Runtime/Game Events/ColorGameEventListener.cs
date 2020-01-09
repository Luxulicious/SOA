using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class ColorGameEventListener : EventListener<ColorGameEvent, ColorUnityEvent, Color>
    {
    }
}