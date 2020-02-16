using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class ColorGameEventListener : GameEventListener<ColorGameEvent, ColorUnityEvent, Color>
    {
    }
}