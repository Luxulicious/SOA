using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class ColorReference : Reference<ColorVariable, Color, ColorUnityEvent, ColorColorUnityEvent>
    {
    }
}