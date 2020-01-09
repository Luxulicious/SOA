using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Color32UnityEvent : UnityEvent<Color32>
    {
    }

    [Serializable]
    public class Color32Color32UnityEvent : UnityEvent<Color32, Color32>
    {
    }

    [Serializable]
    public class Color32ReferenceUnityEvent : UnityEvent<Color32Reference>
    {
    }
}