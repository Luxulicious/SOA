using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class ColorUnityEvent : UnityEvent<Color>
    {
    }

    [Serializable]
    public class ColorColorUnityEvent : UnityEvent<Color, Color>
    {
    }

    [Serializable]
    public class ColorReferenceUnityEvent : UnityEvent<ColorReference>
    {
    }
}